using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(CameraFlipRenderer), PostProcessEvent.AfterStack, "Custom/CameraFlip")]
public sealed class CameraFlip : PostProcessEffectSettings
{

}

[ExecuteAlways]
public sealed class CameraFlipRenderer : PostProcessEffectRenderer<CameraFlip>
{
    public override void Render(PostProcessRenderContext context)
    {
        Debug.Log("HERE");
        context.command.Blit(context.source, context.destination, new Vector2(1, -1), new Vector2(0f, 1f));
    }
}
*/

[ExecuteAlways]
public class CameraEffects : MonoBehaviour
{
    public bool Inverse;
    public Material EffectMaterial;

    private Material _materialInstance;

    void OnEnable()
    {
        SetMaterialInstance();
    }

    public void SetMaterialInstance()
    {
        _materialInstance = new Material(EffectMaterial);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (_materialInstance == null) return;

        _materialInstance.SetInt("_Inverse", Inverse ? 1 : 0);
        Graphics.Blit(src, dest, _materialInstance);
    }
}