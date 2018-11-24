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

    private void Update()
    {

        PlayButton.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        UndoButton.gameObject.SetActive(false);
        if(GameManager.Instance.GameState == GameState.Pause)
        {
            PlayButton.gameObject.SetActive(true);
        }
        if (GameManager.Instance.GameState == GameState.Play)
        {
            PauseButton.gameObject.SetActive(true);
        }
        if (GameManager.Instance.GameState == GameState.Pause)
        {
            PlayButton.gameObject.SetActive(true);
        }
        if (GameManager.Instance.GameState == GameState.Pause)
        {
            PlayButton.gameObject.SetActive(true);
        }
    }

    public void PausePress()
    {

    }

    public void PlayPress()
    {

    }

    public void RestartPress()
    {

    }

    public void UndoPress()
    {

    }
}
