using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHider : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private GameObject _player;

    private void Start()
    {
        _eventBus.OnExerciseExecuteEnd += ShowPlayer;
        _eventBus.OnExerciseInterrupted += ShowPlayer;
        _eventBus.OnExerciseButtonPressed += HidePlayer;
    }
    public void ShowPlayer()
    {
        _player.gameObject.SetActive(true);
    }
    public void HidePlayer()
    {
        _player.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _eventBus.OnExerciseExecuteEnd -= ShowPlayer;
        _eventBus.OnExerciseInterrupted -= ShowPlayer;
        _eventBus.OnExerciseButtonPressed -= HidePlayer;
    }
}
