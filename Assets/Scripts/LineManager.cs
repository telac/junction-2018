using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{

    private LineController _buildLine;
    private Vector2 _initMousePos;
    private Vector2 _origin;
    private bool _clicked;
    private List<LineController> _lines;

    private void Awake()
    {
        _buildLine = GameManager.Instance.LinePool.GetPooledObject().component;
        SetUpBuildLine();
        _origin = new Vector2(0, 0);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            _initMousePos = GetMousePos();
            _clicked = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _clicked = false;
            CreateLine(_initMousePos, GetMousePos());
        }
        DrawTemporaryLine();
    }

    void DrawTemporaryLine()
    {
        if (_clicked)
        {
            _buildLine.Line.SetPosition(0, _initMousePos);
            _buildLine.Line.SetPosition(1, GetMousePos());
        }
        else
        {
            _buildLine.Line.SetPosition(0, _origin);
            _buildLine.Line.SetPosition(1, _origin);
        }
    }

    void SetUpBuildLine()
    {
        _buildLine.Line.SetPosition(0, _origin);
        _buildLine.Line.SetPosition(1, _origin);
    }

    private Vector2 GetMousePos()
    {
        return Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);
    }

    void CreateLine(Vector2 start, Vector2 end)
    {
        // draw line
        var line = GameManager.Instance.LinePool.GetPooledObject().component.Line;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        // add collision
        var collider = GameManager.Instance.LinePool.GetPooledObject().component.Collider;
        collider.enabled = true;
        var points = new Vector2[2];
        points[0] = start;
        points[1] = end;
        collider.points = points;

        //_lines.Add(lineController);
    }

    public void ResetLines() {
        foreach (var line in GameManager.Instance.LinePool.Pool)
        {
            line.component.ReturnToPool();
        }
    }
}
