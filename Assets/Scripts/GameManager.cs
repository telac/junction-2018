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
    public string CurrentLevel;
    public BallPool BallPool;
    public LinePool LinePool;
    public GameState GameState;

    public ParticleSystemPool GoalSFXPool;
    public ParticleSystemPool DeathSFXPool;

    public AudioEffectPool HitAudioPool;
    public AudioEffectPool WinSoundPool;
    public AudioEffectPool UIAudioPool;

    public GameObject FadeUI;
    [HideInInspector]
    public List<Spawner> Spawners;
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

        Spawners = new List<Spawner>();

        CurrentLevel = SceneManager.GetActiveScene().name;
        GameState = GameState.Begin;
    }

    public void ResetLevel()
    {
        // reset ball positions
        foreach (var spawner in Spawners)
        {
            spawner.ResetBalls();
        }
        //LineManager.ResetLines(); 
        // Lines are removed with undo
        GameState = GameState.Begin;
    }

    public void EndLevel(string targetScene = "")
    {
        if (GameState == GameState.ChangeLevel) return;
        if (targetScene == "")
        {
            targetScene = NextLevel(CurrentLevel);
        }

        GameState = GameState.ChangeLevel;

        Spawners.Clear();

        StartCoroutine(StartLevelEndFade(targetScene));
    }

    public IEnumerator StartLevelEndFade(string nextScene)
    {
        yield return new WaitForSeconds(0.5f);
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
        CurrentLevel = sceneName;
        Fade(1.25f, 0f);
        Pause();
    }

    void Update()
    {
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

        if (SceneManager.GetActiveScene().name == "credits") return;
        if (SceneManager.GetActiveScene().name == "mainMenu") return;

        if (GameState == GameState.Play || GameState == GameState.Pause)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                LineManager.Undo();
            }
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
        else if (GameState == GameState.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetLevel();
                Play();
            }
        }
        else if (GameState == GameState.Begin)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Play();
            }
        }
    }

    public void Pause()
    {
        GameState = GameState.Pause;
        foreach (var ball in BallPool.Pool)
        {
            ball.component.Pause();
        }
    }

    public void Play()
    {
        GameState = GameState.Play;
        foreach (var ball in BallPool.Pool)
        {
            ball.component.Play();
        }
    }

    public void Undo()
    {
        LineManager.Undo();
    }

    public void Fade(float time, float target)
    {
        _fadeTime = time;
        _fadeTimer = time;
        _fadeTarget = target;
        _fadeStart = _fadeAmount;
    }

    public void gameOver()
    {
        if (GameState != GameState.GameOver)
        {
            Pause();
            GameState = GameState.GameOver;
        }

    }


    public string NextLevel(string curLvl)
    {
        switch (curLvl)
        {
            case "mainMenu":
                return "final01";
            case "final01":
                return "final02";
            case "final02":
                return "final03";
            case "final03":
                return "final04";
            case "final04":
                return "final05";
            case "final05":
                return "final06";
            case "final06":
                return "final07";
            case "final07":
                return "credits";
            case "final08":
                return "final09";
            case "final09":
                return "credits";
            default:
                return "mainMenu";
        }

    }

}
