using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseRewardAnimationSpawner : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private ExerciseRewardAnimationBehaviour _exerciseRewardAnimationPrefab;
    [SerializeField]
    private int _animationCount;

    private ExerciseRewardAnimationBehaviour[] _allExerciseRewardAnimation;
    private int _currentAnimationIndex;

    private void Awake()
    {
        SpawnAllAnimation();
    }

    private void Start()
    {
        _eventBus.OnExerciseRewardCollected += ShowRewardAnimation;
    }

    public void SpawnAllAnimation()
    {
        _allExerciseRewardAnimation = new ExerciseRewardAnimationBehaviour[_animationCount];

        for (int i = 0; i < _animationCount; i++)
        {
            _allExerciseRewardAnimation[i] = Instantiate(_exerciseRewardAnimationPrefab, transform);
        }
    }

    public void ShowRewardAnimation(Vector3 animationPosition, float goldReward, float strengthReward)
    {
        _allExerciseRewardAnimation[_currentAnimationIndex].GetCurrentReward(animationPosition, goldReward, strengthReward);

        _currentAnimationIndex++;

        if(_currentAnimationIndex == _allExerciseRewardAnimation.Length)
        {
            _currentAnimationIndex = 0;
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnExerciseRewardCollected -= ShowRewardAnimation;
    }
}
