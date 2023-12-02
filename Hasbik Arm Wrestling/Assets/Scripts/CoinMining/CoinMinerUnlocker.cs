using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMinerUnlocker : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private PlayerDataForSave _playerDataForSave;
    [SerializeField]
    private int _moneyToUnlocke;

    public static bool IsMinerUnlocked { get; private set; }
    public static int MoneyToUnlocke { get; private set; }

    private void Awake()
    {
        MoneyToUnlocke = _moneyToUnlocke;

        _eventBus.OnPlayerDataLoaded += GetSavedInlockeData;
    }

    public void UnlockeMiner()
    {
        IsMinerUnlocked = true;
        _eventBus.OnMoneySpended?.Invoke(_moneyToUnlocke);
        _eventBus.OnGoldMinerUnlocked?.Invoke();
    }

    public void GetSavedInlockeData()
    {
        IsMinerUnlocked = _playerDataForSave.IsGoldMinerUnlocked;
    }

    public void ObDestroy()
    {
        _eventBus.OnPlayerDataLoaded -= GetSavedInlockeData;
    }
}
