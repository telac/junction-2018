using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool LightSpawn = true;
    public bool DarkSpawn = true;

    void Awake()
    {
        GameManager.Instance.Spawner = gameObject.GetComponent<Spawner>();
        if (LightSpawn)
        {
            SpawnBall(BallType.Light);
        }
        if (DarkSpawn)
        {
            SpawnBall(BallType.Dark);
        }
    }

    private void SpawnBall(BallType type)
    {
        var ball = GameManager.Instance.BallPool.GetPooledObject();
        ball.component.SetBallType(type);
        ball.component.transform.position = gameObject.transform.position;
        if (type == BallType.Light)
        {
            GameManager.Instance.LightBall = ball.component; 
        }
        else
        {
            GameManager.Instance.DarkBall = ball.component;
        }
        ball.component.Pause();
    }

    public void ResetBalls() {
        if (LightSpawn)
        {
            GameManager.Instance.LightBall.ReturnToPool();
            SpawnBall(BallType.Light);
        }
        if (DarkSpawn)
        {
            GameManager.Instance.DarkBall.ReturnToPool();
            SpawnBall(BallType.Dark);
        }
    }
}
