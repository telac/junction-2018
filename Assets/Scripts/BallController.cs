using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallType
{
    Dark, Light
}

public class BallController : MonoBehaviour, IPoolable
{
    [HideInInspector]
    public BallType BallType;

    public void SetBallType(BallType type)
    {
        BallType = type;
        if (BallType == BallType.Dark)
        {
            gameObject.layer = LayerMask.NameToLayer("Dark");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Light");
        }
    }

    public void ResetState() { }
    public void ReturnToPool() { }
}
