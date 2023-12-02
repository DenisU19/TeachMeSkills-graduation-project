using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroungMusicPlayer : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private SceneFadeManager _fadeManager;
    [SerializeField]
    private BackgroundMusic[] _allBackgroundMusic;
    [SerializeField]
    private BackgroundMusic[] _allArmwrestlingBackgroundMusic;
    [SerializeField]
    private float _musicFadeTime;

    private BackgroundMusic _currentBackgroundMusic;
    private float _musicFadeSpeed;

    private AudioSource _audioSource;
    private float _currentMusicLength;
    private int _currentMusicIndex;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        SelectBackgroundMusic(_allBackgroundMusic);
    }

    private void Start()
    {
        _eventBus.OnRoomCompleted += ArmwrestlingMusicActivator;
        _eventBus.OnNextRoomStarted += MainBackgroundMusicActivator;
    }

    public void SelectBackgroundMusic(BackgroundMusic[] allBackgroundMusic)
    {
        _currentMusicIndex = Random.Range(0, allBackgroundMusic.Length);

        if (_currentBackgroundMusic == null || _currentBackgroundMusic.BackgroundAudio != allBackgroundMusic[_currentMusicIndex].BackgroundAudio)
        {
            _currentBackgroundMusic = allBackgroundMusic[_currentMusicIndex];

            _currentMusicLength = _currentBackgroundMusic.BackgroundAudio.length;

            StartCoroutine(PlayBackgroundMusic(allBackgroundMusic));
        }
        else if (_currentBackgroundMusic.BackgroundAudio == allBackgroundMusic[_currentMusicIndex].BackgroundAudio)
        {
            SelectBackgroundMusic(allBackgroundMusic);
            return;
        }
    }

    public void ArmwrestlingMusicActivator()
    {
        StopAllCoroutines();

        StartCoroutine(MusicFadeStarter(_fadeManager.FadeSpeed * Time.deltaTime, _allArmwrestlingBackgroundMusic));
    }

    public void MainBackgroundMusicActivator()
    {
        StopAllCoroutines();

        StartCoroutine(MusicFadeStarter(_fadeManager.FadeSpeed * Time.deltaTime, _allBackgroundMusic));

    }

    public IEnumerator PlayBackgroundMusic(BackgroundMusic[] allBackgroundMusic)
    {
        _audioSource.volume = 0;
        _musicFadeSpeed = 0;

        _audioSource.clip = _currentBackgroundMusic.BackgroundAudio;

        _audioSource.Play();

        while (_audioSource.volume < _currentBackgroundMusic.MusicVolume)
        {
            _audioSource.volume = Mathf.Lerp(0, _currentBackgroundMusic.MusicVolume, _musicFadeSpeed += _musicFadeTime * Time.deltaTime * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(_currentMusicLength - _musicFadeTime);

        //_musicFadeSpeed = 0;

        //while (_audioSource.volume > 0)
        //{
        //    _audioSource.volume = Mathf.Lerp(_currentBackgroundMusic.MusicVolume, 0, _musicFadeSpeed += _musicFadeTime * Time.deltaTime * Time.deltaTime);

        //    yield return null;
        //}

        StartCoroutine(MusicFadeStarter(Time.deltaTime * Time.deltaTime, allBackgroundMusic));

        //SelectBackgroundMusic(allBackgroundMusic);
    }

    private IEnumerator MusicFadeStarter(float fadeSpeed,BackgroundMusic[] allBackgroundMusic)
    {
        _musicFadeSpeed = 0;

        while (_audioSource.volume > 0)
        {
            _audioSource.volume = Mathf.Lerp(_currentBackgroundMusic.MusicVolume, 0, _musicFadeSpeed += _musicFadeTime * fadeSpeed);

            yield return null;
        }

        SelectBackgroundMusic(allBackgroundMusic);
    }

    private void OnDestroy()
    {
        _eventBus.OnRoomCompleted -= ArmwrestlingMusicActivator;
        _eventBus.OnNextRoomStarted -= MainBackgroundMusicActivator;

    }
}
