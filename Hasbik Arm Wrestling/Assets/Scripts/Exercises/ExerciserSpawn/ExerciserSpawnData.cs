using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciserSpawnData : MonoBehaviour
{
    [SerializeField]
    private ExerciserSpawnChance[] _spawnChances;

    public ExerciserSpawnChance[] SpawnChances => _spawnChances;

    public float _currentSpawnChance { get; private set; }
    public GameObject _currentSpawnObject { get; private set; }

    public void SetCurrentSpawnChance(int spawnChanceIndex)
    {
        _currentSpawnChance = _spawnChances[spawnChanceIndex].SpawnChance;
    }

    public void SetCurrentExerciser()
    {
        _currentSpawnObject = gameObject;
    }
}
