using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(menuName = "GameData/PlayerStats", fileName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    [SerializedDictionary("Player stat name", "Stat value")]
    private SerializedDictionary<HeroStats, float> _allStats;
    [SerializeField]
    [SerializedDictionary("Stats change by level", "Stats coefficient")]
    private SerializedDictionary<HeroStatsForChange, float> _allStatsChangeCoefficients;

    public SerializedDictionary<HeroStats, float> AllStats => _allStats;
    public SerializedDictionary<HeroStatsForChange, float> AllStatsChangeCoefficients => _allStatsChangeCoefficients;

    public enum HeroStats
    {
        WaterCount, Strength, MovementSpeed
    }
   
    public enum HeroStatsForChange
    {
        WaterCount, Strength, StrengthFromExerciser, NextRoomStrength
    }

}

