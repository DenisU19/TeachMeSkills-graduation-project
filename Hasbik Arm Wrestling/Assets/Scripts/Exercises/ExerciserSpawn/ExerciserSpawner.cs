using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciserSpawner : MonoBehaviour
{
    [SerializeField]
    private ExerciserSpawnData[] _exerciserSpawnData;
    [SerializeField]
    private List<Transform> _exerciserSpawnPoints;
    [SerializeField]
    private Transform _exerciserParent;
    [SerializeField]
    private int _minExerciserCountInRoom;

    private List<ExerciserSpawnData> _currenSpawnChances;
    private StrengthCounter _strengthCounter;
    private int _exerciserCount;
    private float _spawnChancesSum;
    private float _currentSpawnChance;

    public int sessionRoomNumber { get; private set; }

    private void Awake()
    {
        _currenSpawnChances = new List<ExerciserSpawnData>();

        _exerciserCount = Random.Range(_minExerciserCountInRoom, _exerciserSpawnPoints.Count);

        sessionRoomNumber = RoomGenerator._sessionRoomNumber;
    }

    public void Start()
    {
        _strengthCounter = FindObjectOfType<StrengthCounter>();

        FindCurrentSpawnChance();
    }

    public void SelectSpawnObject()
    {
        for (int i = 0; i < _exerciserCount; i++)
        {
            _currentSpawnChance = Random.Range(0, _spawnChancesSum);

            foreach (var spawnObject in _currenSpawnChances)
            {
                _currentSpawnChance -= spawnObject._currentSpawnChance;

                if (_currentSpawnChance <= 0)
                {
                    int currentSpawnPointIndex = Random.Range(0, _exerciserSpawnPoints.Count);

                    SpawnExerciser(spawnObject._currentSpawnObject, _exerciserSpawnPoints[currentSpawnPointIndex]);

                    _exerciserSpawnPoints.RemoveAt(currentSpawnPointIndex);

                    break;
                }
            }
        }
    }

    public void FindCurrentSpawnChance()
    {
        for (int i = 0; i < _exerciserSpawnData.Length; i++)
        {
            for (int j = 0; j < _exerciserSpawnData[i].SpawnChances.Length; j++)
            {
                if (_strengthCounter.CurrentStrengthLevel >= _exerciserSpawnData[i].SpawnChances[j].MinPlayerLevel && _strengthCounter.CurrentStrengthLevel <= _exerciserSpawnData[i].SpawnChances[j].MaxPlayerLevel)
                {
                    _exerciserSpawnData[i].SetCurrentSpawnChance(j);
                    _exerciserSpawnData[i].SetCurrentExerciser();

                    _currenSpawnChances.Add(_exerciserSpawnData[i]);
                }
            }  
        }

        SortingCurrentSpawnChanced();
    }

    public void SortingCurrentSpawnChanced()
    {
        for (int i = 0; i < _currenSpawnChances.Count; i++)
        {
            for (int j = 0; j < _currenSpawnChances.Count; j++)
            {
                if(_currenSpawnChances[j]._currentSpawnChance < _currenSpawnChances[i]._currentSpawnChance)
                {
                    ExerciserSpawnData temporarySpawnChances = _currenSpawnChances[j];

                    _currenSpawnChances[j] = _currenSpawnChances[i];

                    _currenSpawnChances[i] = temporarySpawnChances;
                }
            }
        }

        for (int i = 0; i < _currenSpawnChances.Count; i++)
        {
            _spawnChancesSum += _currenSpawnChances[i]._currentSpawnChance;
        }

        SelectSpawnObject();
    }

    public void SpawnExerciser(GameObject objectForSpawn, Transform spawnPosition)
    {
        Instantiate(objectForSpawn, spawnPosition.position, Quaternion.identity, _exerciserParent);
    }
}
