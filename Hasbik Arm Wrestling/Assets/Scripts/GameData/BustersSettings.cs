using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(menuName = "GameData/BustersSettings", fileName = "BustersSettings")]

public class BustersSettings : ScriptableObject
{
    [SerializeField]
    [SerializedDictionary("Buster type", "buster data")]
    private SerializedDictionary<BusterType, BusterData> _allBusters;

    public SerializedDictionary<BusterType, BusterData> AllBusters => _allBusters;

    public enum BusterType
    {
        SodaBuster, BarBuster, EnergeticBuster
    }
}
