using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private PlayerDataForSave _playerDataForSave;
    [SerializeField]
    private SurroundingObjectsTeleport[] _roomPrefabs;
    [SerializeField]
    private Transform _firstRoomPosition;

    private Vector3 _nextRoomPosition;
    private bool _isRoomAlreadySpawned;
    private float _roomsOffset;

    public static List<SurroundingObjectsTeleport> _currentRooms = new List<SurroundingObjectsTeleport>();
    public static SurroundingObjectsTeleport _lastSpawnedRoom;
    public static int _roomCount;
    public static int _lastSpawnedRoomIndex;

    public static int _sessionRoomNumber;

    private void Awake()
    {
        _roomsOffset = _roomPrefabs[0].GetComponent<BoxCollider>().size.x;

        _eventBus.OnPlayerDataLoaded += GetSavedRoomsData;

        _eventBus.OnPlayerInitializated += FirstRoomGeneration;
    }

    private void Start()
    {
        //_eventBus.OnRoomCompleted += RoomGeneration;

        _eventBus.OnNextRoomStarted += RoomGeneration;
    }

    public void FirstRoomGeneration()
    {
        if(_isRoomAlreadySpawned == false)
        {
            _isRoomAlreadySpawned = true;

            _lastSpawnedRoomIndex = Random.Range(0, _roomPrefabs.Length);

            _sessionRoomNumber++;

            _lastSpawnedRoom = Instantiate(_roomPrefabs[_lastSpawnedRoomIndex], _firstRoomPosition.position, Quaternion.identity, transform);

            _currentRooms.Add(_lastSpawnedRoom);

            _nextRoomPosition = _lastSpawnedRoom.transform.position + new Vector3(0f, 0f, _roomsOffset);

            RoomGeneration();

            _currentRooms[0].TeleporObject();

            //_eventBus.OnNextDoorRedrawed?.Invoke(_currentRooms[0].gameObject);

            _eventBus.OnNextRoomSpawned?.Invoke(_currentRooms[0].gameObject, _roomCount);
        }
       
    }

    public void RoomGeneration()
    {
        var newRoomIndex = Random.Range(0, _roomPrefabs.Length);

        _sessionRoomNumber++;

        //if (newRoomIndex == _lastSpawnedRoomIndex)
        //{
        //    RoomGeneration();
        //    return;
        //}

        if (_currentRooms.Count <= 2)
        {
            _lastSpawnedRoom = Instantiate(_roomPrefabs[newRoomIndex], _nextRoomPosition, Quaternion.identity, transform);

            _currentRooms.Add(_lastSpawnedRoom);

            _nextRoomPosition += new Vector3(0f, 0f, _roomsOffset);
        }
        else if(_currentRooms.Count == 3)
        {
            ChangeCurrentRoomsOrder();

            _lastSpawnedRoom = Instantiate(_roomPrefabs[newRoomIndex], _nextRoomPosition, Quaternion.identity, transform);

            _currentRooms[_currentRooms.Count -1] = _lastSpawnedRoom;

            _nextRoomPosition += new Vector3(0f, 0f, _roomsOffset);
        }

        _roomCount++;

        _currentRooms[1].TeleporObject();

        _eventBus.OnNextRoomSpawned?.Invoke(_currentRooms[1].gameObject, _roomCount);    

        //_eventBus.OnNextDoorRedrawed?.Invoke(_currentRooms[1].gameObject);

        _lastSpawnedRoomIndex = newRoomIndex;
    }

    public void ChangeCurrentRoomsOrder()
    {
        Destroy(_currentRooms[0].gameObject);

        for (int i = 0; i < _currentRooms.Count - 1; i++)
        {
            _currentRooms[i] = _currentRooms[i + 1];
        }
    }

    public void GetSavedRoomsData()
    {
        //if (_playerDataForSave.RoomCount > 1)
        //{
        _roomCount = _playerDataForSave.RoomCount - 1;

        _eventBus.OnRoomCountInitialized?.Invoke();

        Debug.Log(_roomCount);
        //}
        //else
        //{
        //    _roomCount = _playerDataForSave.RoomCount;
        //}

        FirstRoomGeneration();
    }

    private void OnDestroy()
    {
        //_eventBus.OnRoomCompleted -= RoomGeneration;

        _eventBus.OnNextRoomStarted -= RoomGeneration;

        _eventBus.OnPlayerDataLoaded -= GetSavedRoomsData;

        _eventBus.OnPlayerInitializated -= FirstRoomGeneration;
    }
}
