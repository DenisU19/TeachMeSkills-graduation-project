using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaBusterCollector : BusterCollector
{
    public override void CollectBuster()
    {
        _eventBus.OnSodaBusterCollected?.Invoke(_busterValue);

        _eventBus.OnSodaCollectAnimationStarted?.Invoke(transform.position);

        base.CollectBuster();
    }
}
