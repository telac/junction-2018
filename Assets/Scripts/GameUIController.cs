using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Button PauseButton;
    public Button PlayButton;
    public Button RestartButton;
    public Button UndoButton;

    private UIButtonController _undoImage;

    private float _energyInitialWidth;
    public Image EnergyBar;

    private Canvas _canvas;

    void Awake()
    {
        _canvas = GetComponentInChildren<Canvas>();
        _energyInitialWidth = EnergyBar.rectTransform.rect.width / _canvas.scaleFactor;

        _undoImage = UndoButton.GetComponentInChildren<UIButtonController>();
    }

    private void Update()
    {
        PlayButton.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        UndoButton.gameObject.SetActive(false);

        EnergyBar.gameObject.SetActive(false);

        // If changing level, hide all UI
        if (GameManager.Instance == null || GameManager.Instance.GameState == GameState.ChangeLevel)
        {
            return;
        }

        EnergyBar.gameObject.SetActive(true);
        var energyT = GameManager.Instance.LineManager.Energy / 30f;
        // If a minimal amount of energy is remaining, round to 0
        if (energyT < 5f / 30f)
        {
            energyT = 0f;
        }
        energyT = Mathf.Clamp(energyT, 0f, 1f);

        var offset = -20 - (1f - energyT) * _energyInitialWidth;
        EnergyBar.rectTransform.offsetMax = new Vector2(offset, EnergyBar.rectTransform.offsetMax.y);

        // Restart is only available if state is Paused or GameOver
        var restartActive = GameManager.Instance.GameState == GameState.Pause || GameManager.Instance.GameState == GameState.GameOver;
        RestartButton.gameObject.SetActive(restartActive);

        // Undo is only available in Begin, and interactable if there are lines to remove
        var undoActive = GameManager.Instance.GameState == GameState.Begin || GameManager.Instance.GameState == GameState.Pause;
        var undoAvailable = GameManager.Instance.LineManager.Lines.Count != 0;
        UndoButton.gameObject.SetActive(undoActive);
        _undoImage.SetActive(undoAvailable);
        UndoButton.interactable = undoAvailable;

        // Play is only available in Begin and Pause
        var playActive = GameManager.Instance.GameState == GameState.Begin || GameManager.Instance.GameState == GameState.Pause;
        PlayButton.gameObject.SetActive(playActive);

        // Pause is enabled only if state is Play
        var pauseActive = GameManager.Instance.GameState == GameState.Play;
        PauseButton.gameObject.SetActive(pauseActive);
    }

    public void PausePress()
    {
        GameManager.Instance.UIAudioPool.GetPooledObject();
        GameManager.Instance.Pause();
    }

    public void PlayPress()
    {
        GameManager.Instance.UIAudioPool.GetPooledObject();
        GameManager.Instance.Play();
    }

    public void RestartPress()
    {
        GameManager.Instance.UIAudioPool.GetPooledObject();
        GameManager.Instance.ResetLevel();
    }

    public void UndoPress()
    {
        GameManager.Instance.UIAudioPool.GetPooledObject();
        GameManager.Instance.Undo();
    }
}
