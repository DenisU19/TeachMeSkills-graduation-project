using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAdder : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private WaterAdderViewRedrawer _waterAdderViewRedrawer;
    [SerializeField]
    private int _waterPrice;

    private void OnEnable()
    {
        CheckBuyPossibility();
    }
    public void CheckBuyPossibility()
    {
        if(WaterCounter.ÑurrentWaterCount == WaterCounter.MaxWaterCount)
        {
            _waterAdderViewRedrawer.DrawFullWaterCount();
            return;
        }

        if (MoneyCounter.GoldCount >= _waterPrice)
        {
            _waterAdderViewRedrawer.RedrawView(_waterPrice, true);
        }
        else
        {
            _waterAdderViewRedrawer.RedrawView(_waterPrice, false);
        }
    }
    public void BuyWater()
    {
        if(MoneyCounter.GoldCount >= _waterPrice)
        {
            _eventBus.OnWaterAdded?.Invoke();
            _eventBus.OnMoneySpended?.Invoke(_waterPrice);
        }

        CheckBuyPossibility();
    }
}
