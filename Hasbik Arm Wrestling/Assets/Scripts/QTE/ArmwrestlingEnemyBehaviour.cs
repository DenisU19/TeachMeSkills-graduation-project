using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmwrestlingEnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private ArmwrestlingEnemyData _armwrestlingEnemyData;

    private EnemyData _currentEnemyData;

    private float _currentEnemyStrength;
    private float _currentCurveTime;
    private float _totalCurveTime;
    private float _maxEnemyStrength;
    private AnimationCurve _currentStrengthChangeLaw;

    private void OnEnable()
    {
        _currentCurveTime = 0;

        SelectEnemyStrength();
    }

    private void Start()
    {
        _eventBus.OnQTEStarted += EnemyActivator;
        _eventBus.OnQTECompleted += EnemyDisactivator;
        _eventBus.OnQTEFailed += EnemyDisactivator;

        enabled = false;
    }

    private void Update()
    {
        AddForce();
    }

    public void SelectEnemyStrength()
    {
        _currentEnemyData = _armwrestlingEnemyData.EnemiesStrengthData[Random.Range(0, _armwrestlingEnemyData.EnemiesStrengthData.Length)];

        _currentEnemyStrength = _currentEnemyData.EnemyStrength;
        _currentStrengthChangeLaw = _currentEnemyData.StrengthChangeLaw;

        _maxEnemyStrength = _currentStrengthChangeLaw.keys[_currentStrengthChangeLaw.keys.Length - 1].value;
        _totalCurveTime = _currentStrengthChangeLaw.keys[_currentStrengthChangeLaw.keys.Length - 1].time;
    }

    public void EnemyActivator()
    {

        enabled = true;
    }
    public void EnemyDisactivator()
    {
        enabled = false;
    }

    public void AddForce()
    {
        if(_currentCurveTime < _totalCurveTime)
        {
            _currentEnemyStrength = _currentEnemyData.EnemyStrength * _currentStrengthChangeLaw.Evaluate(_currentCurveTime) * Time.deltaTime;
        }
        else
        {
            _currentEnemyStrength = _currentEnemyData.EnemyStrength * _maxEnemyStrength * Time.deltaTime;

        }

        _currentCurveTime += Time.deltaTime;

        _eventBus.OnArmwrestlingEnemyActive?.Invoke(_currentEnemyStrength);
    }

    private void OnDestroy()
    {
        _eventBus.OnQTEStarted -= EnemyActivator;
        _eventBus.OnQTECompleted -= EnemyDisactivator;
        _eventBus.OnQTEFailed -= EnemyDisactivator;
    }
}
