using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallType
{
    Dark, Light
}

public class BallController : MonoBehaviour, IPoolable
{
    public LayerMask DarkLayerMask;
    public LayerMask LightLayerMask;

    [HideInInspector]
    public BallType BallType;

    public void SetBallType(BallType type)
    {
        BallType = type;
        if (BallType == BallType.Dark)
        {
            gameObject.layer = DarkLayerMask;
        }
        else
        {
            gameObject.layer = LightLayerMask;
        }
    }

    public void ResetState() { }
    public void ReturnToPool() { }
}
