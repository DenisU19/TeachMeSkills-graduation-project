using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(menuName = "GameData/PlayerUpgradesSettings", fileName = "PlayerUpgradesSettings")]
public class UpgradesSettings : ScriptableObject
{
    [SerializeField]
    [SerializedDictionary("Player upgrade", "upgrade value in percent")]
    private SerializedDictionary<PlayerUpgrade, float> _allUpgrades;
    [SerializeField]
    [SerializedDictionary("Player upgrade cost", "start upgrade cost")]
    private SerializedDictionary<PlayerUpgrade, int> _allUpgradesStartCost;
    [SerializeField]
    [SerializedDictionary("Player upgrade cost coefficient by levels", "upgrade cost coefficient")]
    private SerializedDictionary<PlayerUpgrade, float> _allUpgradesCostCoefficient;

    public SerializedDictionary<PlayerUpgrade, float> AllUpgrades => _allUpgrades;
    public SerializedDictionary<PlayerUpgrade, int> AllUpgradesStartCost => _allUpgradesStartCost;
    public SerializedDictionary<PlayerUpgrade, float> AllUpgradesCostCoefficient => _allUpgradesCostCoefficient;


    public enum PlayerUpgrade
    {
        LegExercises, ArmExercises, Metabolism, Popularity
    }
}
