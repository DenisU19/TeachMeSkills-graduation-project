using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergeticBusterShopItem : BusterShopItem
{
    public override void BuyBuster()
    {
        base.BuyBuster();

        _eventBus.OnEnergeticBusterCollected(1);
    }
}
