using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private AudioSource _soundsSource;

    public void MusicModeSwitcher()
    {
        _musicSource.enabled = !_musicSource.enabled;
    }

    public void SoundModeSwitcher()
    {
        _soundsSource.enabled = !_soundsSource.enabled;
    }
}
