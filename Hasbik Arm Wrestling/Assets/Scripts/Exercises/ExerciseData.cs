using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ExersicePauseTimer))]
public class ExerciseData : MonoBehaviour
{
    [SerializeField]
    protected EventBus _eventBus;
    [SerializeField]
    protected ExerciserStatsUpgrador _statsUpgrador;
    [SerializeField]
    protected WaterCountSettings _waterCountSettings;
    [SerializeField]
    protected Transform _exersiceProgressBarPosition;
    [SerializeField] [Range(1, 999999)]
    protected float _strengthLevelRequirement;
    [SerializeField]
    protected float _addedStrenghtValue;
    [SerializeField]
    protected float _addedGoldValue;
    [SerializeField]
    protected float _subtractWaterValue;
    [SerializeField]
    protected float _exerciseExecutionTime;
    [SerializeField]
    protected float _exercisesPauseTime;
    [SerializeField]
    protected float _bonusCoefficient;

    protected ExerciseQTEResulter _exerciseQteResulter;
    protected AddStrengthEventHandler _addStrengthEventHandler;
    protected PlayerUpgrade _playerUpgrades;
    protected SkinData _playerSkins;
    protected BusterManager _busterManager;
    protected ExersicePauseTimer _pauseTimer;
    protected StrengthCounter _strengthCounter;
    protected HeroCameraMoverItem _heroCameraMoverItem;
    protected ExerciserSpawner _exerciseSpawner;
    protected float _exerciseExecutionSpeed;
    protected float _exerciseExecutionProgress;
    protected float _currentStrengthValue;
    protected float _currentGoldValue;
    protected float _currentWaterValue;
    protected float _currentExecutionTime;
    protected int _exerciseTry;
    protected bool _isExerciseOnPause;
    protected bool _isPlayerNear;

    private void Awake()
    {
        _currentStrengthValue = _addedStrenghtValue;

        _currentGoldValue = _addedGoldValue;

        _currentWaterValue = _subtractWaterValue;

        //_currentExecutionTime = _exerciseExecutionTime;

        _exerciseQteResulter = FindObjectOfType<ExerciseQTEResulter>();

        _addStrengthEventHandler = FindObjectOfType<AddStrengthEventHandler>();

        _pauseTimer = GetComponent<ExersicePauseTimer>();

        _heroCameraMoverItem = GetComponentInChildren<HeroCameraMoverItem>();

        _exerciseSpawner = transform.parent.parent.GetComponent<ExerciserSpawner>();

        _strengthCounter = FindObjectOfType<StrengthCounter>();

        _busterManager = FindObjectOfType<EnergeticBusterManager>(true);
    }

    private void OnEnable()
    {
        _exerciseExecutionSpeed = 1 / _exerciseExecutionTime;
    }

    public virtual void Start()
    {
        CheckStatsUpgrade();

        _eventBus.OnExerciseButtonPressed += ExerciseStarter;
        _eventBus.OnCameraMovedToPlayer += CheckPracticeOpportunity;
        //_eventBus.OnJoystickTouchEnd += CheckPracticeOpportunity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNear = true;

            CheckPracticeOpportunity();
            //enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _eventBus.OnExerciseButtonDisactivated?.Invoke();

            _isPlayerNear = false;
        }
    }

    public virtual void DoExercise()
    {
       
    }

    public void ExerciseStarter()
    {
        if(_isPlayerNear == true)
        {
            enabled = true;

            _heroCameraMoverItem.SetNewCameraTarget();

            _exerciseTry++;
        }
    }

    protected IEnumerator ExerciseReloading()
    {
         enabled = false;

        _isExerciseOnPause = true;

        _eventBus.OnExerciseExecuteEnd?.Invoke();

        _pauseTimer.StartDelayTimerDraw(this, _exercisesPauseTime);

        yield return new WaitForSeconds(_exercisesPauseTime);

        _isExerciseOnPause = false;

        CheckPracticeOpportunity();
    }

    protected void CheckPracticeOpportunity()
    {
        if (_isExerciseOnPause == false && _isPlayerNear == true)
        {
            _eventBus.OnExerciseButtonActivated?.Invoke();
        }

        //if (PlayerInputHandler._isJoystickTouch == false && _isPlayerNear == true && _isExerciseOnPause == false)
        //{
        //    enabled = true;

        //    _eventBus.OnExerciseExecuteStart?.Invoke();
        //}
    }

    public virtual void GetExerciseReward()
    {

    }

    protected void CheckStatsUpgrade()
    {
        foreach (var upgrade in _statsUpgrador.AllUpgrades)
        {
            if (_exerciseSpawner.sessionRoomNumber == 1)
            {
                if (upgrade.Key.minLevel <= RoomGenerator._roomCount && upgrade.Key.maxLevel >= RoomGenerator._roomCount)
                {
                    Mathf.RoundToInt(_strengthLevelRequirement *= upgrade.Value.UpgradesValue[ExerciserUpgradeData.StatsForChange.StrengthLevelRequirement]);
                    Mathf.RoundToInt(_addedStrenghtValue *= upgrade.Value.UpgradesValue[ExerciserUpgradeData.StatsForChange.AddedStrenghtCoefficient]);
                    Mathf.RoundToInt(_subtractWaterValue *= upgrade.Value.UpgradesValue[ExerciserUpgradeData.StatsForChange.SubtractWaterCoefficient]);
                    Mathf.RoundToInt(_addedGoldValue *= upgrade.Value.UpgradesValue[ExerciserUpgradeData.StatsForChange.AddedGoldCoefficient]);
                    break;
                }
            }
            else if (_exerciseSpawner.sessionRoomNumber > 1)
            {
                if (upgrade.Key.minLevel <= RoomGenerator._roomCount + 1 && upgrade.Key.maxLevel >= RoomGenerator._roomCount + 1)
                {
                    Mathf.RoundToInt(_strengthLevelRequirement *= upgrade.Value.UpgradesValue[ExerciserUpgradeData.StatsForChange.StrengthLevelRequirement]);
                    Mathf.RoundToInt(_addedStrenghtValue *= upgrade.Value.UpgradesValue[ExerciserUpgradeData.StatsForChange.AddedStrenghtCoefficient]);
                    Mathf.RoundToInt(_subtractWaterValue *= upgrade.Value.UpgradesValue[ExerciserUpgradeData.StatsForChange.SubtractWaterCoefficient]);
                    Mathf.RoundToInt(_addedGoldValue *= upgrade.Value.UpgradesValue[ExerciserUpgradeData.StatsForChange.AddedGoldCoefficient]);
                    break;
                }
            }
        }
    }

    private void OnDisable()
    {
        //_exerciseExecutionProgress = 0;
    }

    private void OnDestroy()
    {
        _eventBus.OnExerciseButtonPressed -= ExerciseStarter;
        _eventBus.OnCameraMovedToPlayer -= CheckPracticeOpportunity;
        //_eventBus.OnJoystickTouchEnd -= CheckPracticeOpportunity;
    }
}
