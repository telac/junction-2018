using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    private bool _pressed;

    public void PressPlay()
    {
        if (_pressed) return;
        GameManager.Instance.UIAudioPool.GetPooledObject();
        _pressed = true;
        GameManager.Instance.EndLevel();
    }
}
