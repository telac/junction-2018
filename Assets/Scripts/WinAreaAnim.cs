using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAreaAnim : MonoBehaviour
{
    private float _time;
    public float Speed;
    public float Radius;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        transform.localScale = Vector3.one * (0.19f + Radius * Mathf.Sin(Speed * _time));
    }
}
