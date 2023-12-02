using UnityEngine;

public class StrengthCounter : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private PlayerStats _playerStats;
    [SerializeField]
    private StrengthUpgradeEventHandler _upgradeStrengthLevelEvent;
    [SerializeField]
    private StrengthCountRedrawer _strengthCountRedrawer;
    [SerializeField]
    private PlayerDataForSave _playerDataForSave;
    [SerializeField][Range(0,1)]
    private float _strengthAfterDefeat;

    private SoundPlayer _soundPlayer;
    private float _currentStrengthValue;
    private int _strengthToNextLevel;
    private int _currentStrengthLevel = 1;

    public float CurrentStrengthValue => _currentStrengthValue;
    public int CurrentStrengthLevel => _currentStrengthLevel;

    private void Awake()
    {
        _eventBus.OnPlayerDataLoaded += GedSavedStrengthData;

        _soundPlayer = GetComponent<SoundPlayer>();

        _strengthToNextLevel = (int)_playerStats.AllStats[PlayerStats.HeroStats.Strength];

        _strengthCountRedrawer.RedrawStrengthCount(_currentStrengthValue, _strengthToNextLevel, _currentStrengthLevel);

    }

    private void Start()
    {
        _eventBus.OnStrengthAdded += AddStrength;
        _eventBus.OnQTEFailed += ReduceStrengthLevel;
    }

    public void AddStrength(float AddStrength)
    {
        _currentStrengthValue += AddStrength;

        if(_currentStrengthValue >= _strengthToNextLevel)
        {
            AddStrengthLevel();
        }

        _strengthCountRedrawer.RedrawStrengthCount(_currentStrengthValue, _strengthToNextLevel, _currentStrengthLevel);
    }

    public void AddStrengthLevel()
    {
        _currentStrengthLevel++;
        _currentStrengthValue -= _strengthToNextLevel;

        _strengthToNextLevel = Mathf.RoundToInt(_strengthToNextLevel * _playerStats.AllStatsChangeCoefficients[PlayerStats.HeroStatsForChange.Strength]);

        _eventBus.OnStrengthUpgraded();

        _upgradeStrengthLevelEvent.UpgradeLevelEventHandler(_currentStrengthLevel);

        _eventBus.OnPlayerVisualEffectStart?.Invoke(PlayerVisualEffectsData.GameEvent.StrengthLevelUp);

        _soundPlayer.PlayAudio();
    }

    public void ReduceStrengthLevel()
    {
        _currentStrengthLevel--;
        
        _strengthToNextLevel = Mathf.RoundToInt(_strengthToNextLevel / _playerStats.AllStatsChangeCoefficients[PlayerStats.HeroStatsForChange.Strength]);

        _currentStrengthValue = Mathf.RoundToInt(_strengthToNextLevel * _strengthAfterDefeat);

        _strengthCountRedrawer.RedrawStrengthCount(_currentStrengthValue, _strengthToNextLevel, _currentStrengthLevel);
    }

    public void GedSavedStrengthData()
    {
        _currentStrengthLevel = _playerDataForSave.StrengthLevel;
        _currentStrengthValue = _playerDataForSave.CurrentStrengthValue;

        if (_currentStrengthLevel > 1)
        {
            for (int i = 0; i < _currentStrengthLevel; i++)
            {
                _strengthToNextLevel = Mathf.RoundToInt(_strengthToNextLevel * _playerStats.AllStatsChangeCoefficients[PlayerStats.HeroStatsForChange.Strength]);
            }
        }

        _strengthCountRedrawer.RedrawStrengthCount(_currentStrengthValue, _strengthToNextLevel, _currentStrengthLevel);
    }

    private void OnDestroy()
    {
        _eventBus.OnStrengthAdded -= AddStrength;
        _eventBus.OnQTEFailed -= ReduceStrengthLevel;
        _eventBus.OnPlayerDataLoaded -= GedSavedStrengthData;
    }
}
