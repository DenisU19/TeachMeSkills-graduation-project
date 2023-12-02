using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseQTEResulter : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private ExerciseQTEData _exerciseQteData;
    [SerializeField]
    private float _requirementPlayerSpeed;

    private GameObject _firstTapButton;
    private GameObject _currentTapButton;
    private GameObject _nextButtonForTap;
    private float _playerInputSpeed;
    private float _lastButtonTouchTime;
    private float _currentButtonTouchTime;

    public float _currentQteEffect = 1f;
    public float _currentTapDelay;

    private bool _isFirstTap = true;
    private bool _isFirstRound = true;

    public float CurrentQteEffect => _currentQteEffect;

    private void Start()
    {
        _eventBus.OnPlayerTraceFigure += ComparePlayerResults;
        _eventBus.OnPlayerStopTrace += ResetTraceProgress;

        _eventBus.OnPlayerStartTrace += ResulterActivator;
        _eventBus.OnPlayerStopTrace += ResulterDisactivator;
        _eventBus.OnExerciseExecuteEnd += ResetTraceProgress;
        //_eventBus.OnPlayerDisactive += CheckPlayerActivity;

        enabled = false;
    }

    public void ResulterActivator()
    {
        if(enabled == false)
        {
            enabled = true;
        }
    }

    public void ResulterDisactivator()
    {
        enabled = false;
    }

    private void Update()
    {
        _currentTapDelay += Time.deltaTime;

        if (_currentTapDelay >= _requirementPlayerSpeed)
        {
            ResetTraceProgress();
            enabled = false;
        }
    }

    public void ComparePlayerResults(GameObject currentButton, GameObject nextButton ,float touchTime)
    {
        //enabled = false;

        _currentTapDelay = 0;

        if (_isFirstTap)
        {
            Debug.Log("1");
            _firstTapButton = currentButton;
            _nextButtonForTap = nextButton;
            _lastButtonTouchTime = touchTime;

            _isFirstTap = false;

            return;
        }

        if(currentButton == _firstTapButton && _isFirstRound == true)
        {
            Debug.Log("2");

            QteEffectActivate();
            _isFirstRound = false;
        }

        if(currentButton == _nextButtonForTap)
        {
            Debug.Log("3");

            _currentTapButton = currentButton;
            _nextButtonForTap = nextButton;
            _currentButtonTouchTime = touchTime;

            if(_currentButtonTouchTime - _lastButtonTouchTime <= _requirementPlayerSpeed)
            {
                _lastButtonTouchTime = _currentButtonTouchTime;
            }
            else
            {
                ResetTraceProgress();
            }
        }
        else 
        {
            Debug.Log("4");

            ResetTraceProgress();
        }
    }

    //public void CheckPlayerActivity()
    //{
    //    _currentTapDelay = 0;
    //    enabled = true;
    //}

    public void QteEffectActivate()
    {
        _currentQteEffect = 1 + (_exerciseQteData.QteUpgradeValue / 100);

        _eventBus.OnExerciseBoostActive?.Invoke();
    }

    public void ResetTraceProgress()
    {
        _currentTapButton = null;
        _nextButtonForTap = null;
        _currentButtonTouchTime = 0;
        _currentQteEffect = 1f;
        _isFirstTap = true;
        _isFirstRound = true;

        _eventBus.OnExerciseBoostDisactive?.Invoke();
    }

    private void OnDestroy()
    {
        _eventBus.OnPlayerTraceFigure -= ComparePlayerResults;
        _eventBus.OnPlayerStopTrace -= ResetTraceProgress;

        _eventBus.OnPlayerStartTrace -= ResulterActivator;
        _eventBus.OnPlayerStopTrace -= ResulterDisactivator;
        _eventBus.OnExerciseExecuteEnd -= ResetTraceProgress;
    }
}
