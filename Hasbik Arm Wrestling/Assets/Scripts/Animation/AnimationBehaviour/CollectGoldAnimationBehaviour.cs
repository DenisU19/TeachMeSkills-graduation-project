using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGoldAnimationBehaviour : CollectibleAnimationBehaviour
{
    public override void Start()
    {
        _eventBus.OnMoneyCollected += SetCollectibleCount;

        base.Start();
    }

    private void OnDestroy()
    {
        _eventBus.OnMoneyCollected -= SetCollectibleCount;
    }
}
