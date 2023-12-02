using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartExerciseButtonBehavior : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private Button _startExerciseButton;

    private void Start()
    {
        _eventBus.OnExerciseButtonActivated += ButtonActivate;
        //_eventBus.OnCameraMovedToPlayer += ButtonActivate;
        //_eventBus.OnStopExerciseButtonPressed += ButtonActivate;
        //_eventBus.OnExerciseInterrupted += ButtonActivate;
        _eventBus.OnExerciseButtonDisactivated += ButtonDisactive;
    }

    private void ButtonActivate()
    {
        _startExerciseButton.gameObject.SetActive(true);
    }

    private void ButtonDisactive()
    {
        _startExerciseButton.gameObject.SetActive(false);
    }

    public void ExerciseActivate()
    {
        _eventBus.OnExerciseButtonPressed?.Invoke();
        ButtonDisactive();
    }

    private void OnDestroy()
    {
        _eventBus.OnExerciseButtonActivated -= ButtonActivate;
        //_eventBus.OnCameraMovedToPlayer -= ButtonActivate;
        //_eventBus.OnStopExerciseButtonPressed -= ButtonActivate;
        //_eventBus.OnExerciseInterrupted -= ButtonActivate;
        _eventBus.OnExerciseButtonDisactivated -= ButtonDisactive;
    }
}
