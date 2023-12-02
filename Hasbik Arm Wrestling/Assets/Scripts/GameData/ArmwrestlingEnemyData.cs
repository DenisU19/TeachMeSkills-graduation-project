using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "GameData/ArmwrestlingEnemyData", fileName = "ArmwrestlingEnemyData")]
public class ArmwrestlingEnemyData : ScriptableObject
{
    [SerializeField]
    private EnemyData[] _enemiesStrengthData;

    public EnemyData[] EnemiesStrengthData => _enemiesStrengthData;
}

[Serializable]
public class EnemyData
{
    [SerializeField]
    private float _enemyStrength;
    [SerializeField]
    private AnimationCurve _strengthChangeLaw;

    public float EnemyStrength => _enemyStrength;
    public AnimationCurve StrengthChangeLaw => _strengthChangeLaw;
}
