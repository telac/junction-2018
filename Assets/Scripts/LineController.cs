using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour, IPoolable
{
    public LineRenderer Line;
    public EdgeCollider2D Collider;
    public float CollisionWidth;

    public void ResetState()
    {
        gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}