using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallType
{
    Dark, Light
}

public class BallController : MonoBehaviour, IPoolable
{
    [HideInInspector]
    public BallType BallType;

    public void Play() {
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }

    public void Pause() {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }

    public void SetBallType(BallType type)
    {
        BallType = type;
        if (BallType == BallType.Dark)
        {
            gameObject.layer = LayerMask.NameToLayer("Dark");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Light");
        }
    }

    public void ResetState() {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        gameObject.SetActive(true);
    }

    public void ReturnToPool() {
        //Debug.Log(gameObject);
        gameObject.SetActive(false);
    }

    public void Update() {
        if (gameObject.transform.position.y < -11) {
            GameManager.Instance.gameOver();
        }
    }
}
