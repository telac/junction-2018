using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{


    private LineController _buildLine;
    private Vector2 _initMousePos;
    private Vector2 _origin;
    private bool _clicked;
    private float _midPoint;
    private float _inversedMidPoint;
    private List<LineController> _lines = new List<LineController>();


    void Awake()
    {
        GameManager.Instance.LineManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _midPoint = Screen.height / 2;
        _inversedMidPoint = (Screen.height / 4) * 3;
        _origin = new Vector2(0, 0);
        SetUpBuildLine();
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
        _buildLine = GameManager.Instance.LinePool.GetPooledObject().component;
        _buildLine.Line.SetPosition(0, _origin);
        _buildLine.Line.SetPosition(1, _origin);
    }

    private Vector2 GetMousePos()
    {
        var mousPos = Input.mousePosition;
        if (mousPos.y >= _midPoint)
        {
            var pos =  GameManager.Instance.LightCamera.Camera.ScreenToWorldPoint(mousPos);
            return pos;
        } 
        else
        {
            Vector2 inversePos = new Vector2(mousPos.x, _midPoint - mousPos.y);
            var pos = GameManager.Instance.DarkCamera.Camera.ScreenToWorldPoint(inversePos);
            return pos;
        }   
    }

    void CreateLine(Vector2 start, Vector2 end)
    {
        // draw line
        var lineObject = GameManager.Instance.LinePool.GetPooledObject();
        var line = lineObject.component.Line;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        // add collision
        var collider = lineObject.component.Collider;
        collider.enabled = true;
        var points = new Vector2[2];
        points[0] = start;
        points[1] = end;
        collider.points = points;
        _lines.Add(lineObject.component);

    }

    public void Undo() {
        var last = _lines.Count - 1;
        if (last >= 0) 
        {
            _lines[last].ReturnToPool();
            _lines[last].Collider.enabled=false;
            _lines.RemoveAt(last);
        }
    }

    public void ResetLines()
    {
        foreach (var line in GameManager.Instance.LinePool.Pool)
        {
            line.component.ReturnToPool();
            line.component.Collider.enabled = false;
        }
        // Might be a good idea to make build line seperate
        // from rest of the lines, or add a flag to the LineController
        // that specifies whether a line is a build line or not
        Start();
    }
}
