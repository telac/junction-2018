using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public int level = 1;
    public BallPool BallPool;

    public BallCameraController LightCamera;
    public BallCameraController DarkCamera;

    // Use this for initialization;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        RestartLevel();
    }

    void RestartLevel()
    {
        // restart whole level
    }

    void ResetLevel()
    {
        // reset ball positions
    }

    void nextLevel()
    {
        level += 1;
        // load next level
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            RestartLevel();
        }
    }

}
