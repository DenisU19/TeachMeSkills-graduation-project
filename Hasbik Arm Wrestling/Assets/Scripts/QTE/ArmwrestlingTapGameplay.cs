using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmwrestlingTapGameplay : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private GameObject _tapAnimation;
    [SerializeField]
    private TutorialCompleteEventHandler _tutorialCompleteHandler;
    [SerializeField]
    private float _tapForce;
    [SerializeField]
    private float _timeWithoutTap;

    private float _currentTimeWithoutTap;
    private int _tapNumber;
    private bool _isTapPossible;

    private void OnEnable()
    {
        _tapNumber = 0;
        _currentTimeWithoutTap = 0;
    }

    private void Start()
    {
        _eventBus.OnQTEStarted += TapGameplayActivator;
        _eventBus.OnQTEStarted += ShowTapTutorial;

        _eventBus.OnQTEFailed += TapGameplayDisactivator;
        _eventBus.OnQTECompleted += TapGameplayDisactivator;

        TapGameplayDisactivator();
    }

    private void Update()
    {
        _currentTimeWithoutTap += Time.deltaTime;

        if(_currentTimeWithoutTap >= _timeWithoutTap && _tapAnimation.activeInHierarchy == false)
        {
            ShowTapTutorial();
        }
    }

    public void PlayerTap()
    {
        _tapNumber++;

        _currentTimeWithoutTap = 0;

        if(_tapNumber == 1)
        {
            _tutorialCompleteHandler.CompleteTutorialEventHandler("start armwrestling", 3);

        }

        HideTapTutorial();

        _eventBus.OnPlayerTaped?.Invoke(_tapForce);
    }

    public void TapGameplayActivator()
    {
        enabled = true;
    }

    public void TapGameplayDisactivator()
    {
        enabled = false;
    }

    public void ShowTapTutorial()
    {
        _tapAnimation.SetActive(true);
    }
    public void HideTapTutorial()
    {
        _tapAnimation.SetActive(false);
    }

    private void OnDestroy()
    {
        _eventBus.OnQTEStarted -= TapGameplayActivator;
        _eventBus.OnQTEStarted -= HideTapTutorial;

        _eventBus.OnQTEFailed -= TapGameplayDisactivator;
        _eventBus.OnQTECompleted -= TapGameplayDisactivator;
    }


}
