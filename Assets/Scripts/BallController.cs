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

    private bool _spawnedGoalSFX;

    public void Play()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }

    public void Pause()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }

    public void SetBallType(BallType type)
    {
        BallType = type;
        if (BallType == BallType.Dark)
        {
            gameObject.layer = LayerMask.NameToLayer("Dark");
            gameObject.tag = "DarkBall";
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Light");
            gameObject.tag = "LightBall";
        }
    }

    public void ResetState()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        //Debug.Log(gameObject);
        gameObject.SetActive(false);
        _spawnedGoalSFX = false;
    }

    public void Update()
    {
        if (gameObject.transform.position.y < -12)
        {
            GameManager.Instance.gameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WinCollision"))
        {
            if (!_spawnedGoalSFX)
            {
                _spawnedGoalSFX = true;
                var sfx = GameManager.Instance.GoalSFXPool.GetPooledObject();
                sfx.gameObject.transform.position = other.transform.position;
            }
        }
    }
}
