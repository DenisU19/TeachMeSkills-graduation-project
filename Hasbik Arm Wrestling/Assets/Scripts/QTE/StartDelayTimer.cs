using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StartDelayTimer : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private StartDelaySoundManager _soundManager;
    [SerializeField]
    private TextMeshProUGUI _startDelayTimerText;
    [SerializeField]
    private int _startDelay;

    private int _currentDelay;
    public float CurrentGameplayTime { get; private set; }


    private void OnEnable()
    {
        _eventBus.OnFadeDisactivated += DelayTimerStarter;
    }

    private void Update()
    {
        CurrentGameplayTime += Time.deltaTime;
    }

    public void DelayTimerStarter()
    {
        StartCoroutine(StartTimer());
    }

    public void RedrawTimerText(string timerText)
    {
        _startDelayTimerText.text = timerText;
    }
    public IEnumerator StartTimer()
    {
        _currentDelay = _startDelay;

        RedrawTimerText(_currentDelay.ToString());

        _startDelayTimerText.gameObject.SetActive(true);

        _soundManager.PlayAudio();

        while (_currentDelay > 1)
        {
            yield return new WaitForSeconds(1f);

            _currentDelay--;

            _soundManager.PlayAudio();


            RedrawTimerText(_currentDelay.ToString());
        }

        //_soundManager.PlayAudio();

        yield return new WaitForSeconds(1f);

        _soundManager.PlayAudio();

        RedrawTimerText("GO!");

        yield return new WaitForSeconds(1f);

        _startDelayTimerText.gameObject.SetActive(false);

        _eventBus.OnQTEStarted?.Invoke();

    }

    private void OnDisable()
    {
        _eventBus.OnFadeDisactivated -= DelayTimerStarter;
        CurrentGameplayTime = 0;
    }
}
