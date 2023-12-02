using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(menuName = "GameData/WaterCountSettings", fileName = "WaterCountSettings")]
public class WaterCountSettings : ScriptableObject
{
    [SerializeField]
    [SerializedDictionary("Game Part", "Water Coefficient value")]
    private SerializedDictionary<GamePartInfluence, float> _allWaterInfluenceParts;

    public SerializedDictionary<GamePartInfluence, float> AllWaterInfluenceParts => _allWaterInfluenceParts;


    public enum GamePartInfluence
    {
        PlayerMovementSpeed, DoingExerciseSpeed
    }
}
