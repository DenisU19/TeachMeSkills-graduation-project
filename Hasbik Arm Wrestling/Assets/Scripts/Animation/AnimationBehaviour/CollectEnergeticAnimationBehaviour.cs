using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEnergeticAnimationBehaviour : CollectibleAnimationBehaviour
{
    public override void Start()
    {
        _eventBus.OnEnergeticBusterCollected += SetCollectibleCount;

        base.Start();
    }

    private void OnDestroy()
    {
        _eventBus.OnEnergeticBusterCollected -= SetCollectibleCount;
    }
}
