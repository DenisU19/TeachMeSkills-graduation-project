using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using GreenGrey.FirebaseStorage;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameSaver : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private PlayerDataForSave _dataForSave;
    [SerializeField]
    private StrengthCounter _strengthCounter;
    [SerializeField]
    private BusterManager _sodaBusterManager;
    [SerializeField]
    private BusterManager _barBusterManager;
    [SerializeField]
    private BusterManager _energeticBusterManager;
    [SerializeField]
    private SkinData _redBootsSkin;
    [SerializeField]
    private SkinData _strawberrySkin;
    [SerializeField]
    private SkinData _blueJacketSkin;
    [SerializeField]
    private PlayerUpgrade _armExercisesUpgrade;
    [SerializeField]
    private PlayerUpgrade _legExercisesUpgrade;
    [SerializeField]
    private PlayerUpgrade _metabolismUpgrade;
    [SerializeField]
    private PlayerUpgrade _popularityUpgarde;

    private const string PLAYER_DATA_KEY = "playerData";
    private bool _isProgressReset;

    private void Start()
    {
        SaverInitialization();

        GgFirebaseStorage.apiEvents.AuthoriseCompleteEvent += OnAuthoriseCompleteEvent;
        GgFirebaseStorage.apiEvents.DataUploadedEvent += OnDataUploadedEvent;
        GgFirebaseStorage.apiEvents.DataLoadedEvent += OnDataLoadedEvent;

        GgFirebaseStorage.Authorise();
    }

    public void OnApplicationFocus(bool focus)
    {
        if(focus == false)
        {
            SavePlayerData();
        }
    }
    public void GetCurrentData()
    {
        _dataForSave._strengthLevel = _strengthCounter.CurrentStrengthLevel;
        _dataForSave._currentStrengthValue = _strengthCounter.CurrentStrengthValue;
        _dataForSave._roomCount = RoomGenerator._roomCount;
        _dataForSave._sodaBusterCount = _sodaBusterManager.CurrentBusterCount;
        _dataForSave._barBusterCount = _barBusterManager.CurrentBusterCount;
        _dataForSave._energeticBusterCount = _energeticBusterManager.CurrentBusterCount;
        _dataForSave._currentWaterCount = WaterCounter.СurrentWaterCount;
        _dataForSave._maxWaterCount = WaterCounter.MaxWaterCount;
        _dataForSave._currentGoldCount = MoneyCounter.GoldCount;
        _dataForSave._miningGoldValue = CoinMiner.MiningMoneyValue;

        _dataForSave._isBlueJacketSkinBougth = _blueJacketSkin._isSkinBuy;
        _dataForSave._isBlueJacketSkinActive = _blueJacketSkin._isSkinActive;

        _dataForSave._isRedbootSkinBougth = _redBootsSkin._isSkinBuy;
        _dataForSave._isRedbootSkinActive = _redBootsSkin._isSkinActive;

        _dataForSave._isStrawberrySkinBougth = _strawberrySkin._isSkinBuy;
        _dataForSave._isStrawberrySkinActive = _strawberrySkin._isSkinActive;

        _dataForSave._isGoldMinerUnlocked = CoinMinerUnlocker.IsMinerUnlocked;

        _dataForSave._armExercisesUpgradeLevel = _armExercisesUpgrade.UpgradeLevel;
        _dataForSave._legExercisesUpgradeLevel = _legExercisesUpgrade.UpgradeLevel;
        _dataForSave._metabolismUpgradeLevel = _metabolismUpgrade.UpgradeLevel;
        _dataForSave._popularityUpgradeLevel = _popularityUpgarde.UpgradeLevel;
    }

    public void SaverInitialization()
    {
        if (!GgFirebaseStorage.TryInit(true))
        {
            // � ������ ������� - �������� � ��� � ������� �� ������
            Debug.LogError("Cant init sdk. For more information see logs before");
            return;
        }
    }

    private void OnAuthoriseCompleteEvent(bool _isNewPlayer)
    {
        if (_isNewPlayer) // ��������� ������ ������������
        {
            Debug.Log($"New user was registered. Uploading default data to storage");

            var defaultData = GetData();

            GgFirebaseStorage.UploadDataAsync(defaultData);

            _eventBus.OnPlayerInitializated?.Invoke();
        }
        else // ��������� ������������� ������������
        {
            GgFirebaseStorage.LoadData();
        }
    }

    private void OnDataLoadedEvent(Dictionary<string, object> _data)
    {
        Debug.Log($"Data was loaded");
        SetupData(_data);
        _eventBus.OnPlayerDataLoaded?.Invoke();
    }

    private void SetupData(Dictionary<string, object> _data)
    {
        // ���������� ������ (��������� ������� ������� �����)
        if (!_data.TryGetValue(PLAYER_DATA_KEY, out var serialisedData))
        {
            Debug.LogError($"Cant find player data in loaded data");
            return;
        }

        // ������������� ������
        //var dataObj = JsonConvert.DeserializeObject<PlayerDataForSave>((string)serialisedData);
        JsonUtility.FromJsonOverwrite((string)serialisedData, _dataForSave);
        //if (dataObj == null)
        //{
        //    Debug.LogError($"Cant deserialize player data:\n{(string)serialisedData}");
        //    return;
        //}

        //_dataForSave.SetCurrentData(dataObj.StrengthLevel);
    }

    public void LoadPlayerData()
    {
        GgFirebaseStorage.LoadData();
    }

    private Dictionary<string, object> GetData()
    {
        //�������� ������ �� �����
        //_dataForSave.GetCurrentData();
        GetCurrentData();

        //����������� ��
        //var dataJson = JsonConvert.SerializeObject(_dataForSave);
        var dataJson = JsonUtility.ToJson(_dataForSave);

        //����������� � �������, ������������ � Firebase
        return new Dictionary<string, object>
        {
                { PLAYER_DATA_KEY, dataJson }
        };
    }

    public void SavePlayerData()
    {
        var currentPlayerData = GetData();

        // ���������� �� �� ������ Firebase
        GgFirebaseStorage.UploadDataAsync(currentPlayerData);
    }

    private void OnDataUploadedEvent()
    {
        Debug.Log($"Data was uploaded");
    }

    public void ResetPlayerProgress()
    {
        _isProgressReset = true;

        GgFirebaseStorage.DeleteCurrentUser();

        //Caching.ClearCache();

        //SceneManager.LoadScene(0);
    }
}








