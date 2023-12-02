using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmwrestlingStarter : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private Button _playerTapButton;
    [SerializeField]
    private GameObject _winPanel;
    [SerializeField]
    private GameObject _losePanel;

    private void OnEnable()
    {
        if (_winPanel.activeInHierarchy)
        {
            _winPanel.SetActive(false);
        }
        if (_losePanel.activeInHierarchy)
        {
            _losePanel.SetActive(false);
        }
    }
    private void Start()
    {
        _eventBus.OnQTEStarted += TapButtonActivate;
    }

    private void TapButtonActivate()
    {
        _playerTapButton.interactable = true;
    }

    public void OnDestroy()
    {
        _eventBus.OnQTEStarted -= TapButtonActivate;
    }

    private void OnDisable()
    {
        _playerTapButton.interactable = false;

    }
}
