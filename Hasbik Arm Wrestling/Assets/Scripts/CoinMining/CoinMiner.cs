using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMiner : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private EnvironmentObjectStats _environmentObjectStats;
    [SerializeField]
    private PlayerDataForSave _playerDataForSave;
    [SerializeField] [Header("Количество денег для майнинга")]
    private float _miningCount;
    [SerializeField] [Header("Время в секундах, за которое нужно нафармить сумму")]
    private float _miningTime;

    private PlayerUpgrade _playerUpgrade;
    private CoinMiningViewDrawer _coinMiningViewDrawer;
    private SkinData _playerSkins;
    //private IEnumerator _currentMiningProcess;
    private float _moneyPerSecond;

    public static float MiningMoneyValue { get; private set; }
    public static float MiningCount { get; private set; }

    private void Awake()
    {
        //_currentMiningProcess = MoneyMiner();
        _miningTime = _environmentObjectStats.AllStats[EnvironmentObjectStats.EnvironmentStats.BloggerMiningTime];

        _eventBus.OnPlayerDataLoaded += GetSavedMiningMoney;
        _eventBus.OnRoomCountInitialized += SelectMiningSpeed;
        _eventBus.OnQTECompleted += SelectMiningSpeed;
        //SelectMiningSpeed();
    }

    private void Start()
    {
        _coinMiningViewDrawer = GetComponent<CoinMiningViewDrawer>();

        _eventBus.OnGoldMinerUnlocked += MinerActivator;

        _playerUpgrade = FindObjectOfType<PopularityUpgrade>(true);

        _playerSkins = FindObjectOfType<BlueJacketSkin>(true);

        enabled = false;
    }

    private void Update()
    {
       if(MiningMoneyValue < _miningCount)
       {
            MiningMoneyValue += (_moneyPerSecond * _playerUpgrade.CurrentUpgradePercentValue * _playerSkins.CurrentSkinEffectValue * Time.deltaTime);

            //_eventBus.OnMiningMineyCollected?.Invoke();
       }
       else
       {
            MiningMoneyValue = _miningCount;
            enabled = false;
       }

        _eventBus.OnMiningMineyCollected?.Invoke();
    }

    public void SelectMiningSpeed()
    {
        _miningCount = _environmentObjectStats.AllStats[EnvironmentObjectStats.EnvironmentStats.BloggerStartReward];
             
        if(RoomGenerator._roomCount >= 1)
        {
            for (int i = 0; i < RoomGenerator._roomCount; i++)
            {
                _miningCount *= _environmentObjectStats.AllStatsChangeCoefficients[EnvironmentObjectStats.StatsForChange.BloggerReward];
                _miningCount = Mathf.RoundToInt(_miningCount);

            }
        }

        _moneyPerSecond = _miningCount / _miningTime;

        MiningCount = _miningCount;

        if (CoinMinerUnlocker.IsMinerUnlocked && enabled == false)
        {
            MinerActivator();
        }
    }

    public void MinerActivator()
    {
        enabled = true;
        //StartCoroutine(_currentMiningProcess);
    }

    public void CollectMiningMoney()
    {
        //StopCoroutine(_currentMiningProcess);

        _eventBus.OnMoneyCollected?.Invoke((int)MiningMoneyValue);

        MiningMoneyValue = 0;

        _eventBus.OnMiningMineyCollected?.Invoke();

        _eventBus.OnCoinCollectAnimationStarted?.Invoke(transform.position);

        MinerActivator();
    }

    //private IEnumerator MoneyMiner()
    //{
    //    while (MiningMoneyValue < _miningCount)
    //    {
    //        yield return new WaitForSeconds(1f);

    //        MiningMoneyValue += (_moneyPerSecond * _playerUpgrade.CurrentUpgradePercentValue * _playerSkins.CurrentSkinEffectValue);

    //        _eventBus.OnMiningMineyCollected?.Invoke();
    //    }
    //    Debug.Log("gh");
    //}

    public void GetSavedMiningMoney()
    {
        MiningMoneyValue = _playerDataForSave.MiningGoldValue;

        if (_playerDataForSave.IsGoldMinerUnlocked)
        {
            enabled = true;

            _coinMiningViewDrawer.ShowMiningProgressBar();
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnGoldMinerUnlocked -= MinerActivator;
        _eventBus.OnPlayerDataLoaded -= GetSavedMiningMoney;

    }
}
