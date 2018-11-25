using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollText : MonoBehaviour
{
    public Text CreditsText;
    public GameObject ReplayButton;
    private Canvas _canvas;
    public float ScrollSpeed;
    private int _count;
    private float _startY;

    void Awake()
    {
        _count = 1;
        _canvas = GetComponent<Canvas>();
        _startY = CreditsText.rectTransform.position.y;
        ReplayButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.up * Time.deltaTime * ScrollSpeed);
        CreditsText.rectTransform.position += Vector3.up * Time.deltaTime * ScrollSpeed;

        if (_count > 1)
        {
            ReplayButton.SetActive(true);
        }

        if (CreditsText.rectTransform.position.y > 24)
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
            // REEEEEEEEEEEEEEEEEEEEEE
            //var camera = GameManager.Instance.LightCamera.Camera;
            //var aspectRatio = camera.aspect;
            //var bottom = camera.transform.position.y - camera.orthographicSize * aspectRatio;
            //var screenBottom = new Vector2(transform.position.x, bottom - 2);
            //var inWorld = Camera.allCameras[1].ScreenToWorldPoint(screenBottom);
            CreditsText.rectTransform.position = new Vector3(CreditsText.rectTransform.position.x, _startY, CreditsText.rectTransform.position.z);
        }
    }
}
