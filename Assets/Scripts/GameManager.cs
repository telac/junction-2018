using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public string NextLevel;
    public BallPool BallPool;
    public LinePool LinePool;

    [HideInInspector]
    public BallCameraController LightCamera;
    [HideInInspector]
    public BallCameraController DarkCamera;
    [HideInInspector]
    public BallController LightBall;
    [HideInInspector]
    public BallController DarkBall;

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
        foreach (var ball in BallPool.Pool)
        {
            ball.component.ReturnToPool();
        }
        if (NextLevel == "lvl01")
            NextLevel = "lvl02";
        else if (NextLevel == "lvl02")
            NextLevel = "lvl01";
        // load next level
        SceneManager.LoadScene(NextLevel);
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
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            nextLevel();
        }
    }

}
