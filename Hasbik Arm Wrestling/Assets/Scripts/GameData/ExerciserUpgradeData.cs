using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AYellowpaper.SerializedCollections;

[Serializable]
public class ExerciserUpgradeData 
{
    [SerializeField]
    [SerializedDictionary("Stats for change", "Stat coefficient")]
    private SerializedDictionary<StatsForChange, float> _upgradesValue;

    public SerializedDictionary<StatsForChange, float> UpgradesValue => _upgradesValue;


    public enum StatsForChange
    {
        StrengthLevelRequirement, AddedStrenghtCoefficient, AddedGoldCoefficient, SubtractWaterCoefficient
    }
}
