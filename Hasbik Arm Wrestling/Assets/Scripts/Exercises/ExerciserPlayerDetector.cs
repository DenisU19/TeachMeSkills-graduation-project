using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciserPlayerDetector : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;

    private Collider _exerciserCollider;

    private void Awake()
    {
        _exerciserCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        _eventBus.OnCameraMovedToPlayer += TurnOnPlayerDetector;
        _eventBus.OnExerciseButtonPressed += SwitchOffPlayerDetector;
    }

    public void SwitchOffPlayerDetector()
    {
        _exerciserCollider.enabled = false;
    }

    public void TurnOnPlayerDetector()
    {
        _exerciserCollider.enabled = true;
    }

    private void OnDestroy()
    {
        _eventBus.OnCameraMovedToPlayer -= TurnOnPlayerDetector;
        _eventBus.OnExerciseButtonPressed -= SwitchOffPlayerDetector;
    }
}
