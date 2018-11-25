using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUIController : MonoBehaviour
{
    private bool _pressed;
    public void ReplayPressed()
    {
        if (_pressed) return;
        _pressed = true;
        GameManager.Instance.EndLevel();
    }
}
