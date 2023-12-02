using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerDataForSave : MonoBehaviour
{
    public int _strengthLevel;
    public int _currentGoldCount;
    public int _roomCount;
    public int _sodaBusterCount;
    public int _barBusterCount;
    public int _energeticBusterCount;
    public float _currentStrengthValue;
    public float _currentWaterCount;
    public float _maxWaterCount;
    public float _miningGoldValue;

    public bool _isRedbootSkinBougth;
    public bool _isRedbootSkinActive;
    public bool _isStrawberrySkinBougth;
    public bool _isStrawberrySkinActive;
    public bool _isBlueJacketSkinBougth;
    public bool _isBlueJacketSkinActive;
    public bool _isGoldMinerUnlocked;

    public int _armExercisesUpgradeLevel;
    public int _legExercisesUpgradeLevel;
    public int _metabolismUpgradeLevel;
    public int _popularityUpgradeLevel;



    public int StrengthLevel => _strengthLevel;
    public int CurrentGoldCount => _currentGoldCount;
    public int RoomCount => _roomCount;
    public int SodaBusterCount => _sodaBusterCount;
    public int BarBusterCount => _barBusterCount;
    public int EnergeticBusterCount => _energeticBusterCount;
    public float CurrentStrengthValue => _currentStrengthValue;
    public float CurrentWaterCount => _currentWaterCount;
    public float MaxWaterCount => _maxWaterCount;
    public float MiningGoldValue => _miningGoldValue;

    public bool IsRedbootSkinBougth => _isRedbootSkinBougth;
    public bool IsRedbootSkinActive => _isRedbootSkinActive;
    public bool IsStrawberrySkinBougth => _isStrawberrySkinBougth;
    public bool IsStrawberrySkinActive => _isStrawberrySkinActive;
    public bool IsBlueJacketSkinBougth => _isBlueJacketSkinBougth;
    public bool IsBlueJacketSkinActive => _isBlueJacketSkinActive;
    public bool IsGoldMinerUnlocked => _isGoldMinerUnlocked;

    public int ArmExercisesUpgradeLevel => _armExercisesUpgradeLevel;
    public int LegExercisesUpgradeLevel => _legExercisesUpgradeLevel;
    public int MetabolismUpgradeLevel => _metabolismUpgradeLevel;
    public int PopularityUpgradeLevel => _popularityUpgradeLevel;


}
