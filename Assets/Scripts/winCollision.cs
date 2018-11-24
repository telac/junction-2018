using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollision : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;



    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.red;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LightBall")
        {
            _spriteRenderer.color = Color.green;
            GameManager.Instance.EndLevel();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "LightBall")
        {
            _spriteRenderer.color = Color.red;
        }
    }
}
