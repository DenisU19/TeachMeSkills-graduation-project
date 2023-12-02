using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkinSwitcher : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private GameObject[] _enemies;

    private int? _lastEnemyIndex = null;

    private void Start()
    {
        _eventBus.OnRoomCompleted += SelectRandomEnemy;
    }

    public void SelectRandomEnemy()
    {
        int currentEnemyIndex = Random.Range(0, _enemies.Length);

        if (currentEnemyIndex == _lastEnemyIndex)
        {
            SelectRandomEnemy();
            return;
        }

        EnemyActivator(currentEnemyIndex);
    }

    public void EnemyActivator(int enemyIndex)
    {
        if(_lastEnemyIndex != null)
        {
            _enemies[(int)_lastEnemyIndex].SetActive(false);
        }

        _enemies[enemyIndex].SetActive(true);

        _lastEnemyIndex = enemyIndex;
    }


    private void OnDestroy()
    {
        _eventBus.OnRoomCompleted -= SelectRandomEnemy;
    }


}
