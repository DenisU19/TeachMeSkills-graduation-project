using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaBusterShopItem : BusterShopItem
{
    public override void BuyBuster()
    {
        base.BuyBuster();

        _eventBus.OnSodaBusterCollected(1);
    }
}
