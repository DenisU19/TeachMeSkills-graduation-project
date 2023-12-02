using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBusterCollector : BusterCollector
{
    public override void CollectBuster()
    {
        _eventBus.OnBarBusterCollected?.Invoke(_busterValue);

        _eventBus.OnBarCollectAnimationStarted?.Invoke(transform.position);

        base.CollectBuster();
    }
}
