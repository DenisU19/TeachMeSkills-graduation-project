using UnityEngine;
using System;

[Serializable]
public class BusterData 
{
    [SerializeField]
    private float _activityTime;
    [SerializeField]
    private float _effectValue;

    public float ActivityTime => _activityTime;

    public float EffectValue => _effectValue;
    
}
