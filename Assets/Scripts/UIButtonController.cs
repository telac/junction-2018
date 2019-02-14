using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour
{
    private Image _image;
    private bool _active;
    private float _initialAlpha;

    void Awake()
    {
        _image = GetComponent<Image>();
        _initialAlpha = _image.color.a;
    }

    public void SetActive(bool state)
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, state ? 1f : 0.3f);
    }
}
