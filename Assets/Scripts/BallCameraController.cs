using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallCameraController : MonoBehaviour
{
    public BallType BallType;

    public GameObject FollowTarget;
    public float FollowSmoothing;
    public float LeftDeadZone;
    public float RightDeadZone;
    public Camera Camera;

    private Vector3 _initialPosition;

    void Awake()
    {
        if (!GameManager.Instance) return;

        _initialPosition = transform.position;
        Camera = GetComponent<Camera>();

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
        if (SceneManager.GetActiveScene().name == "credits") return;
        if (SceneManager.GetActiveScene().name == "mainMenu") return;

        if (FollowTarget == null) return;

        var aspectRatio = Camera.aspect;
        var leftEdge = transform.position.x - Camera.orthographicSize * aspectRatio * LeftDeadZone;
        var rightEdge = transform.position.x + Camera.orthographicSize * aspectRatio * RightDeadZone;

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
