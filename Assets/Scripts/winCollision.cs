using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winCollision : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.red;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _spriteRenderer.color = Color.green;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _spriteRenderer.color = Color.red;
        }
    }
}
