using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StartDelaySoundData 
{
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField] [Range(0,1)]
    private float _volume;

    public AudioClip AudioClip => _audioClip;
    public float Volume => _volume;

}
