using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _startFieldOfView = 90;
    [SerializeField] private Camera _camera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera.fieldOfView = _startFieldOfView;
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
