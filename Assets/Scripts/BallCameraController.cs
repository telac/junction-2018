using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraController : MonoBehaviour
{
    public BallType BallType;

    public GameObject FollowTarget;
    public float FollowSmoothing;
    public float LeftDeadZone;
    public float RightDeadZone;

    private Camera _camera;
    private Vector3 _initialPosition;

    void Awake()
    {
        if (!GameManager.Instance) return;

        _initialPosition = transform.position;
        _camera = GetComponent<Camera>();

        if (BallType == BallType.Dark)
        {
            GameManager.Instance.DarkCamera = this;
        }
        else
        {
            GameManager.Instance.LightCamera = this;
        }
    }

    void Update()
    {
        if (FollowTarget == null) return;

        var aspectRatio = _camera.aspect;
        var leftEdge = transform.position.x - _camera.orthographicSize * aspectRatio * LeftDeadZone;
        var rightEdge = transform.position.x + _camera.orthographicSize * aspectRatio * RightDeadZone;

        var targetX = FollowTarget.transform.position.x;

        var newX = transform.position.x;

        if (targetX < leftEdge)
        {
            newX -= Mathf.Abs(targetX - leftEdge);
        }

        if (targetX > rightEdge)
        {
            newX += Mathf.Abs(targetX - rightEdge);
        }

        transform.position = new Vector3(newX, _initialPosition.y, _initialPosition.z);
    }
}
