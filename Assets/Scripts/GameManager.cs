using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState
{
    Play, Pause, Build, ChangeLevel
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
    public BallCameraController LightCamera;
    [HideInInspector]
    public BallCameraController DarkCamera;

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

        RestartLevel();
    }

    void RestartLevel()
    {
        // restart whole level
        GameState = GameState.Play;
        //pause(); // after development start game with pause state
    }

    void ResetLevel()
    {
        // reset ball positions
    }

    public void EndLevel(string targetScene = "")
    {
        if (GameState == GameState.ChangeLevel) return;
        if (NextLevel == "lvl01")
            NextLevel = "lvl02";
        else if (NextLevel == "lvl02")
            NextLevel = "lvl01";
        if (targetScene == "")
            targetScene = NextLevel;

        GameState = GameState.ChangeLevel;

        StartCoroutine(StartLevelEndFade(targetScene));
    }

    public IEnumerator StartLevelEndFade(string nextScene)
    {
        //while (true) yield return new WaitForEndOfFrame();

        //if (LevelEndInput == 2) nextScene = "menu";

        //ExitingLevel = true;
        Fade(1.25f, 1f);
        yield return new WaitForSeconds(1.25f);
        //ChangeLevel(nextScene);
        nextLevel(nextScene);
    }

    void nextLevel(string sceneName)
    {
        foreach (var ball in BallPool.Pool)
        {
            ball.component.ReturnToPool();
        }

        // load next level
        SceneManager.LoadScene(sceneName);
        Fade(1.25f, 0f);
        pause();
    }

    void Update()
    {
        if (GameState == GameState.Play || GameState == GameState.Pause) {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetLevel();
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                RestartLevel();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                EndLevel();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GameState == GameState.Pause) {
                    play();
                }
                else if (GameState == GameState.Play) {
                    pause();
                }
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

    private void pause() {
        GameState = GameState.Pause;
        foreach (var ball in BallPool.Pool)
        {
            ball.component.Pause();
        }
    }

    private void play() {
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

}
