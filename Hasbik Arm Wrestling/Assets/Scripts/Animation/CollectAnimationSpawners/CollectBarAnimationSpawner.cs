using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBarAnimationSpawner : CollectibleObjectsAnimationSpawner
{
    void Start()
    {
        _eventBus.OnBarCollectAnimationStarted += ShowCollectAnimation;
    }

    private void OnDestroy()
    {
        _eventBus.OnBarCollectAnimationStarted -= ShowCollectAnimation;
    }
}
