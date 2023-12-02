using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeManager : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private Image _fadePanel;
    [SerializeField]
    private float _fadeSpeed;

    private float _lerpVariable;
    public float FadeSpeed => _fadeSpeed;

    private void Start()
    {
        _eventBus.OnRoomCompleted += FadeStarter;
        _eventBus.OnPlayerTeleportedFinish += FadeHider;


        _eventBus.OnNextRoomStarted += FadeStarter;
        _eventBus.OnCurrentRoomRestarted += LoseFadeStarter;
    }
    public void LoseFadeStarter()
    {
        StartCoroutine(LoseFadeActivate());
    }
    public void FadeStarter()
    {
        StartCoroutine(FadeActivate());
    }

    public void FadeHider()
    {
        StartCoroutine(FadeDisactivate());
    }

    public IEnumerator FadeActivate()
    {
        _lerpVariable = 0;

        _fadePanel.gameObject.SetActive(true);

        while (_fadePanel.color != Color.black)
        {
            yield return null;

            _fadePanel.color = Color.Lerp(Color.clear, Color.black, _lerpVariable += _fadeSpeed * Time.deltaTime);
        }

        yield return null;

        _eventBus.OnFadeActivated?.Invoke();
    }

    public IEnumerator LoseFadeActivate()
    {
        _lerpVariable = 0;

        _fadePanel.gameObject.SetActive(true);

        while (_fadePanel.color != Color.black)
        {
            yield return null;

            _fadePanel.color = Color.Lerp(Color.clear, Color.black, _lerpVariable += _fadeSpeed * Time.deltaTime);
        }

        yield return null;

        _eventBus.OnLoseFadeActivated?.Invoke();
    }

    public IEnumerator FadeDisactivate()
    {
        _lerpVariable = 0;

        while (_fadePanel.color != Color.clear)
        {
            yield return null;

            _fadePanel.color = Color.Lerp(Color.black, Color.clear, _lerpVariable += _fadeSpeed * Time.deltaTime);
        }

        yield return null;

        _fadePanel.gameObject.SetActive(false);

        _eventBus.OnFadeDisactivated?.Invoke();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            FadeStarter();
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnRoomCompleted -= FadeStarter;
        _eventBus.OnPlayerTeleportedStart -= FadeHider;

        _eventBus.OnNextRoomStarted -= FadeStarter;
        _eventBus.OnCurrentRoomRestarted -= LoseFadeStarter;
    }
}
