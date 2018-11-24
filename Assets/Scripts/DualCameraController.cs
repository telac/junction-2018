using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DualCameraController : MonoBehaviour
{
    public LevelConfiguration Configuration;

    public CameraEffects LightCamera;
    public CameraEffects DarkCamera;

    private BallCameraController _lightCameraController;
    private BallCameraController _darkCameraController;

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

        _lightCameraController = LightCamera.GetComponent<BallCameraController>();
        _darkCameraController = DarkCamera.GetComponent<BallCameraController>();
    }

    void Update()
    {
        if (GameManager.Instance && GameManager.Instance.LightBall)
        {
            _lightCameraController.FollowTarget = GameManager.Instance.LightBall.gameObject;
        }
        if (GameManager.Instance && GameManager.Instance.DarkBall)
        {
            _darkCameraController.FollowTarget = GameManager.Instance.DarkBall.gameObject;
        }
    }
}
