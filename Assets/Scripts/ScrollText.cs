using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollText : MonoBehaviour
{
    public Text CreditsText;
    public GameObject ReplayButton;
    public float ScrollSpeed;
    public int FirstLayer;
    public int SecondLayer;
    public int ThirdLayer;


    private Canvas _canvas;
    private float _startY;
    private LinkedList<int> _layers = new LinkedList<int>();
    private LinkedListNode<int> _current;

    void Awake()
    {
        _layers.AddLast(FirstLayer);
        _layers.AddLast(SecondLayer);
        _layers.AddLast(ThirdLayer);
        _current = _layers.First;
        gameObject.layer = _current.Value;

        _canvas = GetComponent<Canvas>();
        _startY = CreditsText.rectTransform.position.y;
        ReplayButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.up * Time.deltaTime * ScrollSpeed);
        CreditsText.rectTransform.position += Vector3.up * Time.deltaTime * ScrollSpeed;

        if (CreditsText.rectTransform.position.y > 24)
        {
            ReplayButton.SetActive(true);
            // nothing to be seen here
            CreditsText.rectTransform.position = new Vector3(CreditsText.rectTransform.position.x, _startY, CreditsText.rectTransform.position.z);
            _current = _current.Next ?? _layers.First;
            gameObject.layer = _current.Value;
        }
    }
}
