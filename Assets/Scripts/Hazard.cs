using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LightBall" || other.tag == "DarkBall")
        {
            if (GameManager.Instance.GameState == GameState.Play)
            {
                var sfx = GameManager.Instance.DeathSFXPool.GetPooledObject();
                sfx.gameObject.transform.position = other.transform.position;
            }

            GameManager.Instance.gameOver();
        }
    }


}
