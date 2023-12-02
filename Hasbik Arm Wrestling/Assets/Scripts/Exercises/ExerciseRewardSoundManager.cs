using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseRewardSoundManager : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private SoundPlayer _exerciseRewardSoundManager;

    private void Start()
    {
        _eventBus.OnExerciseExecuteEnd += PlayRewardSound;
    }

    public void PlayRewardSound()
    {
        _exerciseRewardSoundManager.PlayAudio();
    }

    private void OnDestroy()
    {
        _eventBus.OnExerciseExecuteEnd -= PlayRewardSound;
    }
}
