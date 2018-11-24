using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState
{
    Begin, Play, Pause, Build, ChangeLevel, GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public string NextLevel;
    public BallPool BallPool;
    public LinePool LinePool;
    public GameState GameState;

    public GameObject FadeUI;
    [HideInInspector]
    public Spawner Spawner;
    [HideInInspector]
    public LineManager LineManager;
    [HideInInspector]
    public BallCameraController LightCamera;
    [HideInInspector]
    public BallCameraController DarkCamera;
    [HideInInspector]
    public BallController LightBall;
    [HideInInspector]
    public BallController DarkBall;

    private FadeUIController _fadeUIController;
    private float _fadeAmount;
    private float _fadeTarget;
    private float _fadeTimer;
    private float _fadeTime;
    private float _fadeStart;

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

        var fadeUI = Instantiate(FadeUI);
        fadeUI.transform.parent = transform;
        _fadeUIController = fadeUI.GetComponent<FadeUIController>();
        _fadeUIController.SetFade(0f);

        GameState = GameState.Begin;
        Debug.Log("Press space to play");
        GameState = GameState.Play; //TODO REMOVE BEFORE COMMIT!
    }

    void ResetLevel()
    {
        // reset ball positions
        Spawner.ResetBalls();
        LineManager.ResetLines();
        GameState = GameState.Begin;
        Debug.Log("Press space to play");
    }

    public void EndLevel(string targetScene = "")
    {
        if (GameState == GameState.ChangeLevel) return;
        if (targetScene == "")
        {
            targetScene = NextLevel;
        }

        GameState = GameState.ChangeLevel;
        Debug.Log("GG go to next level");

        StartCoroutine(StartLevelEndFade(targetScene));
    }

    public IEnumerator StartLevelEndFade(string nextScene)
    {
        Fade(1.25f, 1f);
        yield return new WaitForSeconds(1.25f);
        GotoNextLevel(nextScene);
    }

    private void GotoNextLevel(string sceneName)
    {
        // reset balls
        foreach (var ball in BallPool.Pool)
        {
            ball.component.ReturnToPool();
        }
        // reset lines
        LineManager.ResetLines();
        // load next level
        SceneManager.LoadScene(sceneName);
        Fade(1.25f, 0f);
        Pause();
    }

    void Update()
    {
        if (GameState == GameState.Play || GameState == GameState.Pause)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetLevel();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                EndLevel();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GameState == GameState.Pause)
                {
                    Play();
                }
                else if (GameState == GameState.Play)
                {
                    Pause();
                }
            }
        }
        else if (GameState == GameState.GameOver) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetLevel();
                Play();
            }
        }
        else if (GameState == GameState.Begin) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Play();
            }
        }

        if (_fadeTimer > 0f)
        {
            _fadeTimer -= Time.deltaTime;

            var fadeT = 1f - _fadeTimer / _fadeTime;
            _fadeAmount = Mathf.Lerp(_fadeStart, _fadeTarget, fadeT);

            _fadeUIController.SetFade(_fadeAmount);

            if (_fadeTimer <= 0f)
            {
                _fadeTimer = 0f;
                _fadeAmount = _fadeTarget;
            }
        }
    }

    private void Pause()
    {
        Debug.Log("Game paused, press spacebar");
        GameState = GameState.Pause;
        foreach (var ball in BallPool.Pool)
        {
            ball.component.Pause();
        }
    }

    private void Play()
    {
        GameState = GameState.Play;
        foreach (var ball in BallPool.Pool)
        {
            ball.component.Play();
        }
    }

    public void Fade(float time, float target)
    {
        _fadeTime = time;
        _fadeTimer = time;
        _fadeTarget = target;
        _fadeStart = _fadeAmount;
    }

    public void gameOver() {
        if (GameState != GameState.GameOver) {
            Debug.Log("GG skeletons got you");
            Pause();
            GameState = GameState.GameOver;
        }

    }

}
