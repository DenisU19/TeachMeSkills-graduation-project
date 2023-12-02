using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(UpgradeInterfaceRedrawer))]
[Serializable]
public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField]
    protected EventBus _eventBus;
    [SerializeField]
    protected UpgradesSettings _upgradeSettings;
    [SerializeField]
    protected PlayerDataForSave _playerDataForSave;
    [SerializeField]
    protected BuyUpgradeEventHandler _buyUpgradeEventHandler;
    [SerializeField]
    protected string _upgradeName;
    [SerializeField]
    protected string _upgradeDescription;
    [SerializeField]
    protected Image _upgradeIcon;

    protected UpgradeInterfaceRedrawer _upgradesRedrawer;
    protected int _upgradeCost;
    protected int _upgradeLevel = 1;
    protected float _currentUpgradePercentValue = 1;
    public int UpgradeLevel => _upgradeLevel;
    public float CurrentUpgradePercentValue => _currentUpgradePercentValue;

    public virtual void Awake()
    {
        _upgradesRedrawer = GetComponent<UpgradeInterfaceRedrawer>();

        _upgradesRedrawer.SetUpgradeName(_upgradeName);
    }

    private void Start()
    {
        _eventBus.OnNewUpgradeBought += CheckBuyPossibility;
    }

    public virtual void Upgrade()
    {
        _eventBus.OnNewUpgradeBought?.Invoke();
    }

    public virtual void SetNewUpgradeCost()
    {

    }

    public virtual void CheckBuyPossibility()
    {
        if(MoneyCounter.GoldCount >= _upgradeCost)
        {
            _upgradesRedrawer.BuyButtonActivator(true);
        }
        else
        {
            _upgradesRedrawer.BuyButtonActivator(false);
        }
    }

    public virtual void RedrawUpgradeInterface()
    {
        
    }

    private void OnDestroy()
    {
        _eventBus.OnNewUpgradeBought -= CheckBuyPossibility;
    }
}
