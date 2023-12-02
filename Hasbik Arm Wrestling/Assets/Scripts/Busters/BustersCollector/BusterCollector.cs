using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterCollector : MonoBehaviour, ICollectible
{
    [SerializeField]
    protected EventBus _eventBus;
    [SerializeField]
    private SoundPlayer _soundPlayer;
    [SerializeField][Range(1, 100000)]
    protected int _busterValue;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectBuster();
        }
    }

    public virtual void CollectBuster()
    {
       //switch (_busterType)
       //{
       //   case BusterTypes.Energetic:
       //   _eventBus.OnEnergeticBusterCollected?.Invoke();
       //   break; 
          
       //     case BusterTypes.Bar:
       //   _eventBus.OnBarBusterCollected?.Invoke();
       //   break;
            
       //     case BusterTypes.Soda:
       //   _eventBus.OnSodaBusterCollected?.Invoke();
       //   break;    
       //}

        _soundPlayer.PlayAudio();
        Destroy(gameObject);
    }

    //private enum BusterTypes
    //{
    //    Soda, Energetic, Bar
    //}
}
