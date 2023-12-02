using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmwrestlingResulter : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private EnvironmentObjectStats _environmentObjectStats;
    [SerializeField]
    private ArmwrestlingBarRedrawer _armwrestlingRedrawer;
    [SerializeField]
    private GameObject _winPanel;
    [SerializeField]
    private GameObject _losePanel;
    [SerializeField]
    private SoundPlayer _winPanelSound;
    [SerializeField]
    private SoundPlayer _losePanelSound;
    [SerializeField]
    private Button _playerTapButton;
    [SerializeField]
    private Button _winArmwrestlingButton;
    [SerializeField]
    private Button _loseArmwrestlingButton;
    //[SerializeField]
    private float _goldForArmwrestling;


    private void Awake()
    {
        _eventBus.OnQTECompleted += WinArmwrestling;
        _eventBus.OnQTEFailed += LoseArmwrestling;

        CalculateGameReward();
    }

    private void OnEnable()
    {
        _winArmwrestlingButton.interactable = true;
        _loseArmwrestlingButton.interactable = true;
    }

    public void CalculateGameReward()
    {
        _goldForArmwrestling = _environmentObjectStats.AllStats[EnvironmentObjectStats.EnvironmentStats.QTEReward];

        if (RoomGenerator._roomCount > 1)
        {
            for (int i = 1; i < RoomGenerator._roomCount; i++)
            {
               _goldForArmwrestling *= _environmentObjectStats.AllStatsChangeCoefficients[EnvironmentObjectStats.StatsForChange.QTEReward];
            }
        }
    }

    public void WinArmwrestling()
    {
        CalculateGameReward();

        StartCoroutine(WinArmwrestlingBehavior());
    }

    public void LoseArmwrestling()
    {
        StartCoroutine(LoseArmwrestlingBehavior());
    }

    public IEnumerator LoseArmwrestlingBehavior()
    {
        _losePanelSound.PlayAudio();

        _playerTapButton.interactable = false;

        yield return new WaitForSeconds(0.5f);

        _losePanel.SetActive(true);
    }

    public IEnumerator WinArmwrestlingBehavior()
    {
        _winPanelSound.PlayAudio();

        _armwrestlingRedrawer.DrawArmwrestlingReward((int)_goldForArmwrestling);

        _playerTapButton.interactable = false;

        yield return new WaitForSeconds(0.5f);

        _winPanel.SetActive(true);
    }

    public void GoToNextRoom()
    {
        _eventBus.OnNextRoomStarted?.Invoke();

        _eventBus.OnMoneyCollected?.Invoke((int)_goldForArmwrestling);

        _winArmwrestlingButton.interactable = false;
    }

    public void GoToCurrentRoom()
    {
        _eventBus.OnCurrentRoomRestarted?.Invoke();

        _loseArmwrestlingButton.interactable = false;
    }

    private void OnDestroy()
    {
        _eventBus.OnQTECompleted -= WinArmwrestling;
        _eventBus.OnQTEFailed -= LoseArmwrestling;
    }
}
