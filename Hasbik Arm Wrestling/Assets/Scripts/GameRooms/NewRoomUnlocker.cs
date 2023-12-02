using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(DoorStrengthDrawer))]
public class NewRoomUnlocker : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private EnvironmentObjectStats _environmentObjectStats;

    private StrengthCounter _strengthCounter;
    private DoorStrengthDrawer _doorStrengthDrawer;
    private Transform _unlockerParentObject;
    private int _strengthCountToUnlocke;
    private bool _isUnlockePossible;

    private void Awake()
    {
        _doorStrengthDrawer = GetComponent<DoorStrengthDrawer>();


        _unlockerParentObject = transform.parent;

        _eventBus.OnNextRoomSpawned += GetNextRoomStrength;
    }

    private void Start()
    {
        _strengthCounter = FindObjectOfType<StrengthCounter>();
    }

    private void OnCollisionEnter(Collision other)
    {
        CheckUnlockePossibility();

        if (other.transform.CompareTag("Player") && _isUnlockePossible)
        {
            _eventBus.OnRoomCompleted?.Invoke();

            _isUnlockePossible = false;
        }
    }

    public void CheckUnlockePossibility()
    {
        if(_strengthCounter.CurrentStrengthLevel >= _strengthCountToUnlocke)
        {
            _isUnlockePossible = true;
        }
    }

    public void GetNextRoomStrength(GameObject currentRoom, int roomNumber)
    {
        _strengthCountToUnlocke = roomNumber + 1;

        //_strengthCountToUnlocke = (int)_environmentObjectStats.AllStats[EnvironmentObjectStats.EnvironmentStats.NextRoomStrength];

        //if (roomNumber >= 1)
        //{
        //    for (int i = 1; i < roomNumber; i++)
        //    {
        //        _strengthCountToUnlocke = Mathf.RoundToInt(_strengthCountToUnlocke * _environmentObjectStats.AllStatsChangeCoefficients[EnvironmentObjectStats.StatsForChange.NextRoomStrength]);
        //    }
        //}

        _doorStrengthDrawer.GetNextRoomLevel(_strengthCountToUnlocke);

        if (currentRoom == _unlockerParentObject.gameObject)
        {
            _doorStrengthDrawer.DrawerActivate(true);
        }
        else
        {
            _doorStrengthDrawer.DrawerActivate(false);
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnNextRoomSpawned -= GetNextRoomStrength;
    }
}
