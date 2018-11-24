using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BallCameraController : MonoBehaviour
{
    public BallType BallType;

    public Material SceneCameraMaterial;

    // TODO: Tracking

    void Awake()
    {
        if (!GameManager.Instance) return;

        if (BallType == BallType.Dark)
        {
            GameManager.Instance.DarkCamera = this;
        }
        else
        {
            GameManager.Instance.LightCamera = this;
        }
    }
}
