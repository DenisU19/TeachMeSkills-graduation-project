using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInterfaceSwitcher : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private GameObject[] _gameInterfaces;

    private GameObject _currentInterface;
    private void Awake()
    {
        for (int i = 0; i < _gameInterfaces.Length; i++)
        {
            if (_gameInterfaces[i].activeInHierarchy)
            {
                _currentInterface = _gameInterfaces[i];
                break;
            }
        }
    }

    private void Start()
    {
        _eventBus.OnFadeActivated += SwitchInterface;

        _eventBus.OnLoseFadeActivated += SwitchInterface;
    }


    public void SwitchInterface()
    {
        _currentInterface.SetActive(false);

        for (int i = 0; i < _gameInterfaces.Length; i++)
        {
            if (_gameInterfaces[i] != _currentInterface)
            {
                _currentInterface = _gameInterfaces[i];
                _currentInterface.SetActive(true);
                break;
            }
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnFadeActivated -= SwitchInterface;

        _eventBus.OnLoseFadeActivated -= SwitchInterface;
    }
}
