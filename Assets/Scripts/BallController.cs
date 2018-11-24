using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallType
{
    Dark, Light
}

public class BallController : MonoBehaviour, IPoolable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetState() { }
    public void ReturnToPool() { }
}
