using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBusterShopItem : BusterShopItem
{
    public override void BuyBuster()
    {
        base.BuyBuster();

        _eventBus.OnBarBusterCollected(1);
    }
}
