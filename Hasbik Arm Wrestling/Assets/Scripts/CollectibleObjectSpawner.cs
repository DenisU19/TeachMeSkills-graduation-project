using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using System.Linq;

public class CollectibleObjectSpawner : MonoBehaviour
{
    [Header("Сортируй объекты по убыванию шанса их выпадения")]
    [SerializeField] [SerializedDictionary("Collectible object", "drop chance")]
    private SerializedDictionary<GameObject, float> _allSpawnObjects;
    [SerializeField]
    private List<Transform> _collectibleObjectSpawnPoints;
    [SerializeField]
    private Transform _collectibleObjectParent;
    [SerializeField]
    private int _minCollectiblecObjectInRoom;

    private int _objectToSpawnCount;

    private float _dropChancesSum;

    private float _currentDropChance;

    private void Awake()
    {
        foreach (var spawnObject in _allSpawnObjects)
        {
            _dropChancesSum += spawnObject.Value;
        }

        _objectToSpawnCount = Random.Range(_minCollectiblecObjectInRoom, _collectibleObjectSpawnPoints.Count);
    }

    private void Start()
    {
        SelectSpawnObject();
    }

    public void SelectSpawnObject()
    {
        for (int i = 0; i < _objectToSpawnCount; i++)
        {
            _currentDropChance = Random.Range(0, _dropChancesSum);

            foreach (var spawnObject in _allSpawnObjects)
            {
                _currentDropChance -= spawnObject.Value;

                if (_currentDropChance <= 0)
                {
                    int currentSpawnPointIndex = Random.Range(0, _collectibleObjectSpawnPoints.Count);

                    SpawnCollictibleObject(spawnObject.Key, currentSpawnPointIndex);

                    _collectibleObjectSpawnPoints.RemoveAt(currentSpawnPointIndex);

                    break;
                }
            }
        }  
    }

    public void SpawnCollictibleObject(GameObject objectForSpawn, int spawnPointIndex)
    {
        Instantiate(objectForSpawn, _collectibleObjectSpawnPoints[spawnPointIndex].position, Quaternion.identity, _collectibleObjectParent);
    }
}
