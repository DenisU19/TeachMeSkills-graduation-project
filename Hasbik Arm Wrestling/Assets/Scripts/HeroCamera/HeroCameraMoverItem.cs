using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HeroCameraMoverItem : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _currentCamera;
    [SerializeField]
    private float _cameraMovementSpeed;

    private HeroCameraMover _heroCameraMover;

    private void Start()
    {
        _heroCameraMover = FindObjectOfType<HeroCameraMover>();
    }

    public void SetNewCameraTarget()
    {      
        _heroCameraMover.SetNewCameraTarget(_currentCamera, _cameraMovementSpeed);
    }
}
