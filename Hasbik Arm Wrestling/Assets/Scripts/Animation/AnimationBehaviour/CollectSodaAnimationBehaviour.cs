using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSodaAnimationBehaviour : CollectibleAnimationBehaviour
{
    public override void Start()
    {
        _eventBus.OnSodaBusterCollected += SetCollectibleCount;

        base.Start();
    }

    private void OnDestroy()
    {
        _eventBus.OnSodaBusterCollected -= SetCollectibleCount;
    }
}
