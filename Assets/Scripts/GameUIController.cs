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

    private float _energyInitialWidth;
    public Image EnergyBar;

    private Canvas _canvas;

    void Awake()
    {
        _canvas = GetComponentInChildren<Canvas>();
        _energyInitialWidth = EnergyBar.rectTransform.rect.width / _canvas.scaleFactor;
    }

    private void Update()
    {
        PlayButton.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        UndoButton.gameObject.SetActive(false);

        EnergyBar.gameObject.SetActive(false);

        if (GameManager.Instance.GameState == GameState.ChangeLevel)
        {
            return;
        }

        EnergyBar.gameObject.SetActive(true);
        var energyT = GameManager.Instance.LineManager.Energy / 30f;
        energyT = Mathf.Clamp(energyT, 0.01f, 1f);
        var offset = -10 - (1f - energyT) * _energyInitialWidth;
        //EnergyBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _energyInitialWidth * energyT);
        EnergyBar.rectTransform.offsetMax = new Vector2(offset, EnergyBar.rectTransform.offsetMax.y);

        // Restart is always available
        RestartButton.gameObject.SetActive(true);
        RestartButton.interactable = true;

        UndoButton.gameObject.SetActive(true);
        UndoButton.interactable = true;

        // If in begin or play, make restart inactive
        if (GameManager.Instance.GameState == GameState.Begin || GameManager.Instance.GameState == GameState.Play)
        {
            RestartButton.interactable = false;
        }

        if (GameManager.Instance.GameState == GameState.Begin)
        {
            PlayButton.gameObject.SetActive(true);
        }

        if (GameManager.Instance.LineManager.Lines.Count == 0)
        {
            UndoButton.interactable = false;
        }

        if (GameManager.Instance.GameState == GameState.Pause)
        {
            PlayButton.gameObject.SetActive(true);
        }
        if (GameManager.Instance.GameState == GameState.Play)
        {
            PauseButton.gameObject.SetActive(true);
        }
    }

    public void PausePress()
    {
        GameManager.Instance.Pause();
    }

    public void PlayPress()
    {
        GameManager.Instance.Play();
    }

    public void RestartPress()
    {
        GameManager.Instance.ResetLevel();
    }

    public void UndoPress()
    {
        GameManager.Instance.Undo();
    }
}
