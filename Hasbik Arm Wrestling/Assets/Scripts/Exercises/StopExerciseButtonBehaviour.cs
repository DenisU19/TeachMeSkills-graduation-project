using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopExerciseButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private Button _stopExerciseButton;

    private void Start()
    {
        _eventBus.OnCameraMovedToExerciser += ButtonActivate;
        //_eventBus.OnExerciseButtonPressed += ButtonActivate;
        _eventBus.OnExerciseExecuteEnd += ButtonDisactive;
    }

    private void ButtonActivate()
    {
        _stopExerciseButton.gameObject.SetActive(true);
    }

    private void ButtonDisactive()
    {
        _stopExerciseButton.gameObject.SetActive(false);
    }

    public void ExerciseDisactivate()
    {
        _eventBus.OnStopExerciseButtonPressed?.Invoke();
        ButtonDisactive();
    }

    private void OnDestroy()
    {
        _eventBus.OnCameraMovedToExerciser -= ButtonActivate;
        //_eventBus.OnExerciseButtonPressed -= ButtonActivate;
        _eventBus.OnExerciseExecuteEnd -= ButtonDisactive;
    }
}
