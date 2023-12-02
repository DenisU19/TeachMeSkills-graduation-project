using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickHider : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private GameObject _playerJoystick;

    private void Start()
    {
        _eventBus.OnExerciseButtonPressed += HideJoysctick;
        _eventBus.OnStopExerciseButtonPressed += ShowJoystick;
        _eventBus.OnExerciseExecuteEnd += ShowJoystick;
    }

    public void HideJoysctick()
    {
        _playerJoystick.SetActive(false);
    }

    public void ShowJoystick()
    {
        _playerJoystick.SetActive(true);
    }

    private void OnDestroy()
    {
        _eventBus.OnExerciseButtonPressed -= HideJoysctick;
        _eventBus.OnStopExerciseButtonPressed -= ShowJoystick;
        _eventBus.OnExerciseExecuteEnd -= ShowJoystick;
    }
}
