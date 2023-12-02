using UnityEngine;
using System;
using AYellowpaper.SerializedCollections;

public class StartDelaySoundManager : MonoBehaviour
{

    [SerializeField]
    [SerializedDictionary("SoundOrder", "Sound")]
    private SerializedDictionary<SoundOrder, StartDelaySoundData> _allSounds;
    [SerializeField]
    private AudioSource _audioSource;

    private SoundOrder _currentSoundOrder;
    private int _allSoundCount;
    private int _currentSoundIndex;

    private void Awake()
    {
        _allSoundCount = Enum.GetValues(typeof(SoundOrder)).Length;
    }

    public void PlayAudio()
    {
        _currentSoundOrder = (SoundOrder)Enum.GetValues(typeof(SoundOrder)).GetValue(_currentSoundIndex);
        //_audioSource.Stop();
        _audioSource.PlayOneShot(_allSounds[_currentSoundOrder].AudioClip, _allSounds[_currentSoundOrder].Volume);

        _currentSoundIndex++;

        if (_currentSoundIndex == _allSoundCount)
        {
            _currentSoundIndex = 0;
        }
    }

    public enum SoundOrder 
    {
        Three, Two, One, GO
    }
}
