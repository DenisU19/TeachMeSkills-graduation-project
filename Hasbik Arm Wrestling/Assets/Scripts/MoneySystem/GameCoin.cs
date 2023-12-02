using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoin : Money, ICollectible
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private SoundPlayer _soundPlayer;
    [SerializeField][Range(1, 100000)]
    private int _coinValue;

    public override void CollectObject()
    {
        _eventBus.OnMoneyCollected?.Invoke(_coinValue);

        _eventBus.OnCoinCollectAnimationStarted?.Invoke(transform.position);

        _soundPlayer.PlayAudio();
        Destroy(gameObject);
    }
}
