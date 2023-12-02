using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BusterShopViewRedrawer))]
public class BusterShopItem : MonoBehaviour
{
    [SerializeField]
    protected EventBus _eventBus;
    [SerializeField]
    protected string _busterName;
    [SerializeField]
    protected string _busterDecription;
    [SerializeField]
    protected int _busterCost;

    protected BusterShopViewRedrawer _shopRedrawer;

    private void Awake()
    {
        _shopRedrawer = GetComponent<BusterShopViewRedrawer>();

        _shopRedrawer.DrawBusterData(_busterName, _busterDecription, _busterCost.ToString());
    }

    private void OnEnable()
    {
        CheckBuyPossibility();
    }

    private void Start()
    {
        _eventBus.OnBusterBougth += CheckBuyPossibility;
    }

    public virtual void BuyBuster()
    {
        _eventBus.OnMoneySpended(_busterCost);

        _eventBus.OnBusterBougth?.Invoke();

    }

    public void CheckBuyPossibility()
    {
        if(MoneyCounter.GoldCount >= _busterCost)
        {
            _shopRedrawer.DrawBuyPossibility(true);
        }
        else
        {
            _shopRedrawer.DrawBuyPossibility(false);
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnBusterBougth -= CheckBuyPossibility;
    }
}
