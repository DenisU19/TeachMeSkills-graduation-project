using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(menuName = "GameData/PlayerVisualEffectsData", fileName = "PlayerVisualEffectsData")]

public class PlayerVisualEffectsData : ScriptableObject
{
    [SerializeField]
    [SerializedDictionary("Visual effect trigger", "Visual effect")]
    private SerializedDictionary<GameEvent, ParticleSystem> _playerVisualEffects;

    public SerializedDictionary<GameEvent, ParticleSystem> PlayerVisualEffects => _playerVisualEffects;

    public enum GameEvent
    {
        UseBarBuster, UseSodaBuster, UseEnergeticBuster, StrengthLevelUp
    }
}
