using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSodaAnimationSpawner : CollectibleObjectsAnimationSpawner
{
    void Start()
    {
        _eventBus.OnSodaCollectAnimationStarted += ShowCollectAnimation;
    }

    private void OnDestroy()
    {
        _eventBus.OnSodaCollectAnimationStarted -= ShowCollectAnimation;
    }
}
