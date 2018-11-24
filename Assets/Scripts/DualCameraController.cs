using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DualCameraController : MonoBehaviour
{
    public LevelConfiguration Configuration;

    public CameraEffects LightCamera;
    public CameraEffects DarkCamera;

    void OnEnable()
    {
        if (Configuration == null)
        {
            Debug.LogWarning("Level's Dual Cameras does not have a color configuration set.");
            return;
        }

        LightCamera.EffectMaterial = Configuration.LightMaterial;
        DarkCamera.EffectMaterial = Configuration.DarkMaterial;

        LightCamera.SetMaterialInstance();
        DarkCamera.SetMaterialInstance();
    }
}
