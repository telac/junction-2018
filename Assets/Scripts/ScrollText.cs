using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour
{
    public float ScrollSpeed;
    private int _count;
    // Start is called before the first frame update
    void Start()
    {
        _count = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * ScrollSpeed);

        if(transform.position.y > 30)
        {
            _count += 1;
            if (_count % 2 == 0)
            {
                gameObject.layer = 10;
            }
            if (_count % 3 == 0)
            {
                gameObject.layer = 0;
            }
            else
            {
                gameObject.layer = 9;
            }

            // sori henri
            var camera = Camera.allCameras[1];
            var aspectRatio = camera.aspect;
            var bottom = transform.position.y - camera.orthographicSize * aspectRatio;
            var screenBottom = new Vector2(transform.position.x, bottom - 50);
            var inWorld = Camera.allCameras[1].ScreenToWorldPoint(screenBottom);
            transform.position = screenBottom;
        }
    }
}
