using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audio;
    [SerializeField]
    [Range(0, 1)]
    private float _audioVolume;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GameObject.Find("SoundsPlayer").GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_audio, _audioVolume);
    }
}
