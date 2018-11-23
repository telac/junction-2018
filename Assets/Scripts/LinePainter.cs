using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePainter : MonoBehaviour
{
    public LineRenderer BuildLine;

    private Vector2 _initMousePos;
    private Vector2 _origin;
    private bool _clicked;
    private AnimationCurve _buildCurve = new AnimationCurve();

    void Start()
    {
        SetUpBuildLine();
        _origin = new Vector2(0, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _initMousePos = GetMousePos();
            _clicked = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            _clicked = false;
            DrawPermanentLine(_initMousePos, GetMousePos());
        }
        DrawTemporaryLine();
    }

    void SetUpBuildLine()
    {

        _buildCurve.AddKey(0, 0.1f);
        _buildCurve.AddKey(1, 0.1f);
        BuildLine.widthCurve = _buildCurve;
        BuildLine.SetPosition(0, _origin);
        BuildLine.SetPosition(1, _origin);

    }
    void DrawTemporaryLine()
    {
        if (_clicked)
        {
            BuildLine.SetPosition(0, _initMousePos);
            BuildLine.SetPosition(1, GetMousePos());
        }
        else
        {
            BuildLine.SetPosition(0, _origin);
            BuildLine.SetPosition(1, _origin);
        }
    }

    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }



    public void DrawPermanentLine(Vector2 start, Vector2 end)
    {
        // someone else probably knows better way to generate gameObjects...
        GameObject newLine = new GameObject();
        LineRenderer permanentLine = newLine.AddComponent<LineRenderer>();
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0, 0.1f);
        curve.AddKey(1, 0.1f);
        permanentLine.widthCurve = curve;
        permanentLine.material = new Material(Shader.Find("Sprites/Default"));
        permanentLine.SetPosition(0, start);
        permanentLine.SetPosition(1, end);
    }
}
