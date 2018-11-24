using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraController : MonoBehaviour
{
    public BallType BallType;

    // TODO: Tracking

    void Awake()
    {
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
