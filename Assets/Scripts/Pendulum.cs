using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float offset;
    private Vector3 _startPosition;
    void Awake() {
        _startPosition = gameObject.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _startPosition + new Vector3(0.0f, 4.0f * Mathf.Sin(Time.time + offset), 0.0f);
    }
}
