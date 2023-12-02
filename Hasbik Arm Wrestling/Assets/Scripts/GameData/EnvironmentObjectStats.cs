using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(menuName = "GameData/EnvironmentObjectStats", fileName = "EnvironmentObjectStats")]
public class EnvironmentObjectStats : ScriptableObject
{
    [SerializeField]
    [SerializedDictionary("Stat name", "Stat value")]
    private SerializedDictionary<EnvironmentStats, float> _allStats;
    [SerializeField]
    [SerializedDictionary("Stats change by level", "Stats coefficient")]
    private SerializedDictionary<StatsForChange, float> _allStatsChangeCoefficients;

    public SerializedDictionary<EnvironmentStats, float> AllStats => _allStats;
    public SerializedDictionary<StatsForChange, float> AllStatsChangeCoefficients => _allStatsChangeCoefficients;



    public enum EnvironmentStats
    {
        /*NextRoomStrength,*/ QTEReward, BloggerStartReward, BloggerMiningTime
    }

    public enum StatsForChange
    {
        /*NextRoomStrength,*/ QTEReward, BloggerReward
    }
}
