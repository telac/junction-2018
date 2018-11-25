using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    public GameObject PlayButton;

    private bool _pressed;

    public void PressPlay()
    {
        if (_pressed) return;

        _pressed = true;
        PlayButton.SetActive(false);
        GameManager.Instance.EndLevel();
    }
}
