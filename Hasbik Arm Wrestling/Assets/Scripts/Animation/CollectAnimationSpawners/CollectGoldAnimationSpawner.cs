using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGoldAnimationSpawner : CollectibleObjectsAnimationSpawner
{
    void Start()
    {
        _eventBus.OnCoinCollectAnimationStarted += ShowCollectAnimation;
    }

    private void OnDestroy()
    {
        _eventBus.OnCoinCollectAnimationStarted -= ShowCollectAnimation;
    }
}
