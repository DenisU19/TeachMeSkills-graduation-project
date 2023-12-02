using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObjectsAnimationSpawner : MonoBehaviour
{
    [SerializeField]
    protected EventBus _eventBus;
    [SerializeField]
    protected CollectibleAnimationBehaviour _collectibleAnimationPrefab;
    [SerializeField]
    protected int _animationCount;

    protected CollectibleAnimationBehaviour[] _collectAnimations;

    protected int _currentAnimationIndex;

    private void Awake()
    {
        _collectAnimations = new CollectibleAnimationBehaviour[_animationCount];

        for (int i = 0; i < _animationCount; i++)
        {
            _collectAnimations[i] = Instantiate(_collectibleAnimationPrefab, transform);
        }
    }


    public void ShowCollectAnimation(Vector3 animationPosition)
    {
        _collectAnimations[_currentAnimationIndex].GetAnimationPosition(animationPosition);

        _currentAnimationIndex++;

        if (_currentAnimationIndex == _animationCount)
        {
            _currentAnimationIndex = 0;
        }
    }
}
