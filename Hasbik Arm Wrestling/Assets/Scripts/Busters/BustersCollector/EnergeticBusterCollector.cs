using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergeticBusterCollector : BusterCollector
{
    public override void CollectBuster()
    {
        _eventBus.OnEnergeticBusterCollected?.Invoke(_busterValue);

        _eventBus.OnEnergeticCollectAnimationStarted?.Invoke(transform.position);

        base.CollectBuster();
    }
}
