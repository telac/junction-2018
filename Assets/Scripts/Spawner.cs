using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool LightSpawn = true;
    public bool DarkSpawn = true;

    void Awake()
    {
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
    }
}
