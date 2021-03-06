﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineManager : MonoBehaviour
{

    public List<LineController> Lines;
    public float Energy;
    public const float MINIMUM_COST = 5.0f;
    public const float ENERGY_CAP = 30.0f;

    private LineController _buildLine;
    private Vector2 _initMousePos;
    private Vector2 _origin;
    private bool _clicked;
    private float _midPoint;
    private float _threshold;
    private float _inversedMidPoint;


    void Awake()
    {
        if (GameManager.Instance.LineManager == null)
        {
            GameManager.Instance.LineManager = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Energy = ENERGY_CAP;
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
        if (SceneManager.GetActiveScene().name == "credits") return;
        if (SceneManager.GetActiveScene().name == "mainMenu") return;

        var mPos = Input.mousePosition;

        if (mPos.y > _midPoint + _threshold || mPos.y < _midPoint - _threshold)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _initMousePos = GetMousePos();
                _clicked = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            var start = _initMousePos;
            var end = GetMousePos();
            var dist = Vector2.Distance(start, end);
            if (_clicked && Energy > MINIMUM_COST && dist > 1f)
            {
                CreateLine(start, end);
            }
            // this is just to make sure that build line doesnt stay visible
            // if you delete all lines
            _buildLine.Line.SetPosition(0, _origin);
            _buildLine.Line.SetPosition(1, _origin);
            _clicked = false;
        }
        DrawTemporaryLine();
    }

    void DrawTemporaryLine()
    {
        if (_clicked && Energy > MINIMUM_COST)
        {
            var end = GetMousePos();
            if (Vector2.Distance(_initMousePos, end) > Energy)
            {
                var absDirection = (end - _initMousePos);
                var norDirection = absDirection / absDirection.magnitude;
                end = _initMousePos + norDirection * (Energy);
            }
            _buildLine.Line.SetPosition(0, _initMousePos);
            _buildLine.Line.SetPosition(1, end);
        }
    }

    void SetUpBuildLine()
    {
        _buildLine = GameManager.Instance.LinePool.GetPooledObject().component;
    }

    private Vector2 GetMousePos()
    {
        var mousPos = Input.mousePosition;
        if (mousPos.y >= _midPoint)
        {
            var pos = GameManager.Instance.LightCamera.Camera.ScreenToWorldPoint(mousPos);
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
        if (Vector2.Distance(_initMousePos, GetMousePos()) > Energy)
        {
            var absDirection = (end - start);
            var norDirection = absDirection / absDirection.magnitude;
            end = start + norDirection * (Energy);
        }
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        Energy -= (end - start).magnitude + MINIMUM_COST;

        // add collision
        var collider = lineObject.component.Collider;
        collider.enabled = true;
        var points = new Vector2[2];
        points[0] = start;
        points[1] = end;
        collider.points = points;

        // keep track of lines
        Lines.Add(lineObject.component);
    }

    public void Undo()
    {
        var last = Lines.Count - 1;
        if (last >= 0)
        {
            // restore energy
            var p1 = Lines[last].Line.GetPosition(0);
            var p2 = Lines[last].Line.GetPosition(1);
            Energy += Vector2.Distance(p1, p2) + MINIMUM_COST;

            // deactivate line
            Lines[last].ReturnToPool();
            Lines[last].Collider.enabled = false;
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
