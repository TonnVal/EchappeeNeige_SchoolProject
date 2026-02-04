using Components.SODB;
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera.fieldOfView = 90f;
        GameEventService.OnFieldOfViewUpdated += UpdateFOV;
    }
    private void OnDestroy()
    {
        GameEventService.OnFieldOfViewUpdated -= UpdateFOV;
    }

    private void UpdateFOV(float fov)
    {
        _camera.fieldOfView = fov;
    }
}
