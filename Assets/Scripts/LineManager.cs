using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public List<LineController> Lines;
    public float Energy;

    private LineController _buildLine;
    private Vector2 _initMousePos;
    private Vector2 _origin;
    private bool _clicked;
    private float _midPoint;
    private float _threshold;
    private float _inversedMidPoint;
    

    void Awake()
    {
        if(GameManager.Instance.LineManager == null)
        {
            GameManager.Instance.LineManager = this;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Energy = 30f;
        Lines = new List<LineController>();
        _midPoint = Screen.height / 2;
        _threshold = Screen.height * 0.05f;
        _inversedMidPoint = (Screen.height / 4) * 3;
        _origin = new Vector2(0, 0);
        SetUpBuildLine();
    }

    // Update is called once per frame
    void Update()
    {
        var mPos = Input.mousePosition;
        
        if (mPos.y > _midPoint + _threshold || mPos.y < _midPoint - _threshold)
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
        
    }

    void DrawTemporaryLine()
    {
        if (_clicked)
        {
            var end = GetMousePos();
            if(Vector2.Distance(_initMousePos, end) > Energy) 
            {
                var absDirection = (end - _initMousePos);
                var norDirection = absDirection / absDirection.magnitude;
                end = _initMousePos + norDirection * (Energy);
            }
            _buildLine.Line.SetPosition(0, _initMousePos);
            _buildLine.Line.SetPosition(1, end);
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
        if(Vector2.Distance(_initMousePos, GetMousePos()) > Energy) 
        {
            var absDirection = (end - start);
            var norDirection = absDirection / absDirection.magnitude;
            end = start + norDirection * (Energy);
        }
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        Energy -= (end - start).magnitude;

        // add collision
        var collider = lineObject.component.Collider;
        collider.enabled = true;
        var points = new Vector2[2];
        points[0] = start;
        points[1] = end;
        collider.points = points;
        Lines.Add(lineObject.component);



    }

    public void Undo() {
        var last = Lines.Count - 1;
        if (last >= 0)  
        {
            var p1 = Lines[last].Line.GetPosition(0);
            var p2 = Lines[last].Line.GetPosition(1);
            Energy += Vector2.Distance(p1, p2);
            Lines[last].ReturnToPool();
            Lines[last].Collider.enabled=false;
            Lines.RemoveAt(last);
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
