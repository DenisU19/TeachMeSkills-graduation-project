using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(menuName = "GameData/ExerciserStatsUpgrade", fileName = "ExerciserStatsUpgrade")]

public class ExerciserStatsUpgrador : ScriptableObject
{
    //[SerializeField]
    //[SerializedDictionary("level for change", "Stat value")]
    //private SerializedDictionary<float, ExerciserUpgradeData> _allUpgrades;

    //public SerializedDictionary<float, ExerciserUpgradeData> AllUpgrades => _allUpgrades;


    [SerializeField]
    [SerializedDictionary("room number", "upgrade coefficient")]
    private SerializedDictionary<LevelsForUpgrade, ExerciserUpgradeData> _allUpgrades;

    public SerializedDictionary<LevelsForUpgrade, ExerciserUpgradeData> AllUpgrades => _allUpgrades;
}

[System.Serializable]
public class LevelsForUpgrade
{
    [SerializeField]
    private float _minLevel;
    [SerializeField]
    private float _maxLevel;

    public float minLevel => _minLevel;
    public float maxLevel => _maxLevel;
}
