using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBarAnimationBehaviour : CollectibleAnimationBehaviour
{
    public override void Start()
    {
        _eventBus.OnBarBusterCollected += SetCollectibleCount;

        base.Start();
    }

    private void OnDestroy()
    {
        _eventBus.OnBarBusterCollected -= SetCollectibleCount; 
    }
}
