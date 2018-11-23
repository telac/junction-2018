using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraFlip : MonoBehaviour
{
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, new Vector2(1, -1), new Vector2(0f, 1f));
    }
}
