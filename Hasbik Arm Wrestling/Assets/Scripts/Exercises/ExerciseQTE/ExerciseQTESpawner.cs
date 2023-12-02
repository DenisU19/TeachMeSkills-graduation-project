using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseQTESpawner : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private GameObject[] _allFigures;

    private int _currentFigureIndex;
    public static bool isFirstSpawn = true;

    private void Start()
    {
        _eventBus.OnExerciseButtonPressed += SelectRandomFigure;
        _eventBus.OnExerciseExecuteEnd += HideFigure;
        _eventBus.OnStopExerciseButtonPressed += HideFigure;
    }

    public void SelectRandomFigure()
    {
        _currentFigureIndex = Random.Range(0, _allFigures.Length);

        _allFigures[_currentFigureIndex].SetActive(true);
    }

    public void HideFigure()
    {
        _allFigures[_currentFigureIndex].SetActive(false);
    }

    private void OnDestroy()
    {
        _eventBus.OnExerciseButtonPressed -= SelectRandomFigure;
        _eventBus.OnExerciseExecuteEnd -= HideFigure;
        _eventBus.OnStopExerciseButtonPressed -= HideFigure;
    }
}
