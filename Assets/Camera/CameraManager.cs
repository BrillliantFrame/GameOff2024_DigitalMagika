using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField]
    private CinemachineCamera _mainCamera;

    [SerializeField]
    private List<CinemachineCamera> _cameras;

    private int _currentIndex = -1;

    void Start()
    {
        _mainCamera.gameObject.SetActive(true);

        foreach (var camera in _cameras)
        {
            camera.gameObject.SetActive(false);
        }
    }

    public void ActivateCamera(int cameraIndex)
    {
        _cameras[cameraIndex].gameObject.SetActive(true);
        if (_currentIndex != -1)
            _cameras[_currentIndex].gameObject.SetActive(false);
        _mainCamera.gameObject.SetActive(false);
        _currentIndex = cameraIndex;
    }

    public void ActivateMainCamera()
    {
        _mainCamera.gameObject.SetActive(true);
        _cameras[_currentIndex].gameObject.SetActive(false);
        _currentIndex = -1;
    }
}
