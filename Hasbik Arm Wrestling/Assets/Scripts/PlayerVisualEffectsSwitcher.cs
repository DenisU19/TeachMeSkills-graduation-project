using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualEffectsSwitcher : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private PlayerVisualEffectsData _playerEffects;
    [SerializeField]
    private Transform _playerEffectsPosition;

    private void Start()
    {
        _eventBus.OnPlayerVisualEffectStart += VisualEffectActivate;
    }

    public void VisualEffectActivate(PlayerVisualEffectsData.GameEvent gameEventEffect)
    {
        Instantiate(_playerEffects.PlayerVisualEffects[gameEventEffect], _playerEffectsPosition.position, Quaternion.identity, _playerEffectsPosition);
    }

    private void OnDestroy()
    {
        _eventBus.OnPlayerVisualEffectStart -= VisualEffectActivate;
    }
}
