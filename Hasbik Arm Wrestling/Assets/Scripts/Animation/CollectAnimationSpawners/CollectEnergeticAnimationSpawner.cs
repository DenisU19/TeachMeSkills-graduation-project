using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEnergeticAnimationSpawner : CollectibleObjectsAnimationSpawner
{
    void Start()
    {
        _eventBus.OnEnergeticCollectAnimationStarted += ShowCollectAnimation;
    }

    private void OnDestroy()
    {
        _eventBus.OnEnergeticCollectAnimationStarted -= ShowCollectAnimation;
    }
}
