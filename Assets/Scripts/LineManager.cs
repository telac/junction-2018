﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{

    private LineController _buildLine; 
    private Vector2 _initMousePos;
    private Vector2 _origin;
    private bool _clicked;
    //prefab these too?
    private AnimationCurve _buildCurve = new AnimationCurve();

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
            Debug.Log("hello");
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
        _buildCurve.AddKey(0, 0.1f);
        _buildCurve.AddKey(1, 0.1f);
        _buildLine.Line.widthCurve = _buildCurve;   
        _buildLine.Line.SetPosition(0, _origin);
        _buildLine.Line.SetPosition(1, _origin);

    }

    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void CreateLine(Vector2 start, Vector2 end)
    {
        var line = GameManager.Instance.LinePool.GetPooledObject().component.Line;
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0, 0.1f);
        curve.AddKey(1, 0.1f);
        line.widthCurve = curve;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }
}
