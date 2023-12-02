using UnityEngine;
using System;

[Serializable]
public class BackgroundMusic 
{
    [SerializeField]
    private AudioClip _backgroundAudio;
    [SerializeField] [Range(0,1)]
    private float _musicVolume;

    public AudioClip BackgroundAudio => _backgroundAudio;

    public float MusicVolume => _musicVolume;
} 
