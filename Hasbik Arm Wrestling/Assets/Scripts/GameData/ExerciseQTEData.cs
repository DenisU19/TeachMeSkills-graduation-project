using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/ExerciseQTEUpgradeData", fileName = "ExerciseQTEUpgradeData")]
public class ExerciseQTEData : ScriptableObject
{
    [SerializeField]
    private float _qteUpgradeValue;

    public float QteUpgradeValue => _qteUpgradeValue;
}
