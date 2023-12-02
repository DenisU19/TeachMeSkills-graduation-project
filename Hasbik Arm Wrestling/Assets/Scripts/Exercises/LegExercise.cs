using UnityEngine;

public class LegExercise : ExerciseData
{
    public override void Start()
    {
        base.Start();

        _eventBus.OnStopExerciseButtonPressed += StopExercise;

        _playerUpgrades = FindObjectOfType<LegExercisesUpgrade>(true);

        _playerSkins = FindObjectOfType<RedBootsSkin>(true);

        enabled = false;
    }
    private void Update()
    {
        //if (PlayerInputHandler._isJoystickTouch == false)
        //{
        DoExercise();
        //}
        //else
        //{
        //    _eventBus.OnExerciseInterrupted?.Invoke();

        //    enabled = false;
        //}
    }

    public void StopExercise()
    {
        GetExerciseReward();

        enabled = false;
    }
    public override void DoExercise()
    {
        if (WaterCounter.ÑurrentWaterCount > 0)
        {
            if(_strengthCounter.CurrentStrengthLevel - _strengthLevelRequirement > 0)
            {
                _exerciseExecutionProgress += _exerciseExecutionSpeed * _playerUpgrades.CurrentUpgradePercentValue * _playerSkins.CurrentSkinEffectValue * _busterManager.CurrentBusterEffectValue * _exerciseQteResulter.CurrentQteEffect * Time.deltaTime;

                _eventBus.OnWaterSpended?.Invoke(_subtractWaterValue * _exerciseExecutionSpeed / _exerciseExecutionTime * _exerciseQteResulter.CurrentQteEffect * Time.deltaTime);
            }
            else
            {
                _exerciseExecutionProgress += _exerciseExecutionSpeed * ((float)_strengthCounter.CurrentStrengthLevel / _strengthLevelRequirement) * _playerUpgrades.CurrentUpgradePercentValue * _playerSkins.CurrentSkinEffectValue * _busterManager.CurrentBusterEffectValue * _exerciseQteResulter.CurrentQteEffect * Time.deltaTime;

                _eventBus.OnWaterSpended?.Invoke(_subtractWaterValue * _exerciseExecutionSpeed / _exerciseExecutionTime * ((float)_strengthCounter.CurrentStrengthLevel / _strengthLevelRequirement) * _exerciseQteResulter.CurrentQteEffect * Time.deltaTime);
            }
        }
        else
        {
            _exerciseExecutionProgress += _exerciseExecutionSpeed * ((float)_strengthCounter.CurrentStrengthLevel / _strengthLevelRequirement) * _playerUpgrades.CurrentUpgradePercentValue * _playerSkins.CurrentSkinEffectValue * _busterManager.CurrentBusterEffectValue * (1 - (_waterCountSettings.AllWaterInfluenceParts[WaterCountSettings.GamePartInfluence.DoingExerciseSpeed] / 100)) * _exerciseQteResulter.CurrentQteEffect * Time.deltaTime;
        }

        _eventBus.OnExerciseExecuting?.Invoke(_exerciseExecutionProgress / _exerciseExecutionTime, _exersiceProgressBarPosition);

        if (_exerciseExecutionProgress >= _exerciseExecutionTime)
        {
            GetExerciseReward();
            //_eventBus.OnWaterSpended?.Invoke(_subtractWaterValue);
            //_eventBus.OnStrengthAdded?.Invoke(_addedStrenghtValue);
            //_eventBus.OnMoneyCollected?.Invoke((int)_addedGoldValue);
            //_eventBus.OnExerciseExecuteEnd?.Invoke();
            //_addStrengthEventHandler.AddStrengthEventhandler("Leg exercise", (int)_addedStrenghtValue);

            //_exerciseExecutionProgress = 0;

            //StartCoroutine(ExerciseReloading());
        }
    }

    public override void GetExerciseReward()
    {
        if (_isPlayerNear)
        {
            if (_exerciseExecutionProgress >= _exerciseExecutionTime)
            {
                if (_exerciseTry == 1)
                {
                    _currentStrengthValue = _addedStrenghtValue;

                    _currentGoldValue = _addedGoldValue;

                    //_currentWaterValue = _subtractWaterValue;

                    _currentExecutionTime = _exerciseExecutionTime;

                    _eventBus.OnStrengthAdded?.Invoke(Mathf.RoundToInt(_addedStrenghtValue * _bonusCoefficient));
                    _eventBus.OnMoneyCollected?.Invoke(Mathf.RoundToInt(_addedGoldValue * _bonusCoefficient));
                    _eventBus.OnExerciseExecuteEnd?.Invoke();
                    _eventBus.OnExerciseRewardCollected(transform.position, Mathf.RoundToInt(_addedGoldValue * _bonusCoefficient), Mathf.RoundToInt(_addedStrenghtValue * _bonusCoefficient));

                    _addStrengthEventHandler.AddStrengthEventhandler("Leg exercise", Mathf.RoundToInt(_addedStrenghtValue * _bonusCoefficient));

                    _exerciseExecutionProgress = 0;

                    StartCoroutine(ExerciseReloading());

                    _isPlayerNear = false;

                    _exerciseTry = 0;
                }
                else
                {
                    _eventBus.OnStrengthAdded?.Invoke(Mathf.RoundToInt(_currentStrengthValue));
                    _eventBus.OnMoneyCollected?.Invoke(Mathf.RoundToInt(_currentGoldValue));
                    _eventBus.OnExerciseExecuteEnd?.Invoke();
                    _eventBus.OnExerciseRewardCollected(transform.position, Mathf.RoundToInt(_currentGoldValue), Mathf.RoundToInt(_currentStrengthValue));
                    _addStrengthEventHandler.AddStrengthEventhandler("Leg exercise", Mathf.RoundToInt(_currentStrengthValue));

                    _exerciseExecutionProgress = 0;

                    StartCoroutine(ExerciseReloading());

                    _isPlayerNear = false;

                    _exerciseTry = 0;

                    _currentStrengthValue = _addedStrenghtValue;

                    _currentGoldValue = _addedGoldValue;

                    //_currentWaterValue = _subtractWaterValue;

                    _currentExecutionTime = _exerciseExecutionTime;
                }
            }
            else
            {
                //_currentStrengthValue *= _exerciseExecutionProgress / _exerciseExecutionTime;

                //_currentGoldValue *= _exerciseExecutionProgress / _exerciseExecutionTime;

                //_currentWaterValue *= _exerciseExecutionProgress / _exerciseExecutionTime;

                _currentExecutionTime *= _exerciseExecutionProgress / _exerciseExecutionTime;

                //_eventBus.OnWaterSpended?.Invoke(_subtractWaterValue * _bonusCoefficient);
                _eventBus.OnStrengthAdded?.Invoke(Mathf.RoundToInt(_currentStrengthValue * _exerciseExecutionProgress / _exerciseExecutionTime));
                _eventBus.OnMoneyCollected?.Invoke(Mathf.RoundToInt(_currentGoldValue * _exerciseExecutionProgress / _exerciseExecutionTime));
                _eventBus.OnExerciseInterrupted?.Invoke();
                _eventBus.OnExerciseRewardCollected(transform.position, Mathf.RoundToInt(_currentGoldValue * _exerciseExecutionProgress / _exerciseExecutionTime), Mathf.RoundToInt(_currentStrengthValue * _exerciseExecutionProgress / _exerciseExecutionTime));

                _addStrengthEventHandler.AddStrengthEventhandler("Leg exercise", Mathf.RoundToInt(_currentStrengthValue * (_exerciseExecutionProgress / _exerciseExecutionTime)));

                //_exerciseExecutionProgress = 0;

                _currentStrengthValue = Mathf.RoundToInt(_currentStrengthValue * (1 - (_exerciseExecutionProgress / _exerciseExecutionTime)));

                _currentGoldValue = Mathf.RoundToInt(_currentGoldValue * (1 - (_exerciseExecutionProgress / _exerciseExecutionTime)));

                //_currentWaterValue = _subtractWaterValue - _currentWaterValue;

                //_currentExecutionTime = _addedStrenghtValue * (1 - (_exerciseExecutionProgress / _exerciseExecutionTime));
                //_currentExecutionTime = _exerciseExecutionTime - _currentExecutionTime;
            }
            //_eventBus.OnCoinCollectAnimationStarted?.Invoke(transform.position);
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnStopExerciseButtonPressed -= StopExercise;
    }
}
