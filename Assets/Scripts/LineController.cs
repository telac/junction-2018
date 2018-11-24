using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour, IPoolable
{
    public LineRenderer Line;
    public EdgeCollider2D Collider;
    public float CollisionWidth;


    void Start()
    {

    }


    void Update()
    {

    }
    public void ResetState() { }
    public void ReturnToPool() { }




}