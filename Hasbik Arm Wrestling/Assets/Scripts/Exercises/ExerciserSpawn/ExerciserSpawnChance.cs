using UnityEngine;
using System;

[Serializable]
public class ExerciserSpawnChance 
{
    [SerializeField]
    private float _minPlayerLevel;
    [SerializeField]
    private float _maxPlayerLevel;
    [SerializeField] [Range(0,100)]
    private float _spawnChance;

    public float MinPlayerLevel => _minPlayerLevel;

    public float MaxPlayerLevel => _maxPlayerLevel;

    public float SpawnChance => _spawnChance;
}
