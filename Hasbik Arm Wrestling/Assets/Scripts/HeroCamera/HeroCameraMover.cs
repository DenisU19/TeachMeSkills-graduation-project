using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HeroCameraMover : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    public CinemachineBrain _heroCameraBrain;

    private float _currentCameraMovementTime;
    private CinemachineVirtualCamera _currentVirtualCamera;
    private float _defaultCameraMoveSpeed;

    private void Start()
    {
        _eventBus.OnExerciseInterrupted += MoveCameraToHero;
        _eventBus.OnExerciseExecuteEnd += MoveCameraToHero;

        enabled = false;
    }
   
    public IEnumerator CameraMoverToExerciser(CinemachineVirtualCamera currentVirtualCamera, float cameraMovementSpeed)
    {
        _currentVirtualCamera = currentVirtualCamera;
        _currentCameraMovementTime = cameraMovementSpeed;
        _heroCameraBrain.m_DefaultBlend.m_Time = _currentCameraMovementTime;
        _currentVirtualCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(_currentCameraMovementTime);

        _eventBus.OnCameraMovedToExerciser?.Invoke();

    }
    public void SetNewCameraTarget(CinemachineVirtualCamera currentVirtualCamera, float cameraMovementSpeed)
    {
        StartCoroutine(CameraMoverToExerciser(currentVirtualCamera, cameraMovementSpeed));
    }

    public void MoveCameraToHero()
    {
        StartCoroutine(CameraMoverToHero());
    }

    private IEnumerator CameraMoverToHero()
    {
        _currentVirtualCamera.gameObject.SetActive(false);

        //_currentVirtualCamera = null;

        yield return new WaitForSeconds(_currentCameraMovementTime);

        _heroCameraBrain.m_DefaultBlend.m_Time = _defaultCameraMoveSpeed;

        _eventBus.OnCameraMovedToPlayer?.Invoke();
    }

    private void OnDestroy()
    {
        _eventBus.OnExerciseInterrupted -= MoveCameraToHero;
        _eventBus.OnExerciseExecuteEnd -= MoveCameraToHero;
    }
}
