using UnityEngine;
using TMPro;

public class DoorStrengthDrawer : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private ParticleSystem _unlockeDoorEffectPrefab;
    [SerializeField]
    private Transform _requiredStrengthTextPosition;
    [SerializeField]
    private string _doorIncription;

    private ParticleSystem _unlockeDoorEffect;
    private TextMeshProUGUI _nextRoomStrengthText;
    private StrengthCounter _strengthCounter;
    private Camera _mainCamera;
    private float _strengthToUnlocke;

    private void Awake()
    {
        _mainCamera = GameObject.Find("HeroCamera").GetComponent<Camera>();
        _nextRoomStrengthText = GameObject.Find("NextRoomStrengthText").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        _eventBus.OnStrengthUpgraded += HideDoorDraw;
        _eventBus.OnQTEFailed += ShowDoorDraw;
        _eventBus.OnQTEFailed += DestroyOpenDoorEffect;
        _eventBus.OnQTECompleted += DestroyOpenDoorEffect;

        _strengthCounter = FindObjectOfType<StrengthCounter>();

        HideDoorDraw();
    }

    private void Update()
    {
        _nextRoomStrengthText.transform.position = _mainCamera.WorldToScreenPoint(_requiredStrengthTextPosition.position);
    }

    public void DrawerActivate(bool isDrawerActive)
    {
        enabled = isDrawerActive;

        if (isDrawerActive)
        {
            _nextRoomStrengthText.enabled = true;
        }
    }

    public void GetNextRoomLevel(float nextRoomLevel)
    {
        _strengthToUnlocke = nextRoomLevel;
        _nextRoomStrengthText.text = $"{_doorIncription} {nextRoomLevel}";
    }

    public void HideDoorDraw()
    {
        if (_strengthToUnlocke <= _strengthCounter.CurrentStrengthLevel && enabled == true)
        {
            _nextRoomStrengthText.enabled = false;

            _unlockeDoorEffect = Instantiate(_unlockeDoorEffectPrefab, _requiredStrengthTextPosition.position, Quaternion.identity, transform);
        }
    }

    public void ShowDoorDraw()
    {
        _nextRoomStrengthText.enabled = true;
    }

    public void DestroyOpenDoorEffect()
    {
        if(_unlockeDoorEffect != null)
        {
            Destroy(_unlockeDoorEffect.gameObject);
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnStrengthUpgraded -= HideDoorDraw;
        _eventBus.OnQTEFailed -= ShowDoorDraw;
        _eventBus.OnQTEFailed -= DestroyOpenDoorEffect;
        _eventBus.OnQTECompleted -= DestroyOpenDoorEffect;
    }
}
