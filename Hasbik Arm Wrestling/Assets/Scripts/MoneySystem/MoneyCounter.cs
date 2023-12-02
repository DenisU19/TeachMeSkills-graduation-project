using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private PlayerDataForSave _playerDataForSave;

    public static int GoldCount { get; private set; }

    private void Awake()
    {
        _eventBus.OnPlayerDataLoaded += GetSavedGoldData;
    }

    private void Start()
    {
        _eventBus.OnMoneyCollected += AddCoins;
        _eventBus.OnMoneySpended += SpendCoins;
    }

    public void AddCoins(int coinCount)
    {
        GoldCount += coinCount;
        _eventBus.OnGoldMoneyViewRedraw.Invoke(GoldCount.ToString());
    }

    public void SpendCoins(int spendedMoney)
    {
        GoldCount -= spendedMoney;
        _eventBus.OnGoldMoneyViewRedraw.Invoke(GoldCount.ToString());
    }

    public void GetSavedGoldData()
    {
        GoldCount = _playerDataForSave.CurrentGoldCount;
        _eventBus.OnGoldMoneyViewRedraw.Invoke(GoldCount.ToString());
    }

    private void OnDestroy()
    {
        _eventBus.OnMoneyCollected -= AddCoins;
        _eventBus.OnMoneySpended -= SpendCoins;
        _eventBus.OnPlayerDataLoaded -= GetSavedGoldData;
    }
}
