using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private CinemachineVirtualCamera _playerCamera;
    [SerializeField]
    private Transform _pointToTeleport;

    private Vector3 _lastPlayerPosition;
    private bool _isQTE;

    private void Start()
    {
        _eventBus.OnFadeActivated += TeleporterToQTE;
        _eventBus.OnFadeActivated += TeleporterToMainGame;

        _eventBus.OnLoseFadeActivated += TeleporterToCurrentRoom;
    }

    public void TeleporterToQTE()
    {
        StartCoroutine(TeleportToQTE());
    }

    public void TeleporterToCurrentRoom()
    {
        StartCoroutine(TeleportToCurrentRoom());
    }

    public void TeleporterToMainGame()
    {
        StartCoroutine(TeleportToMainGame());
    }

    public IEnumerator TeleportToCurrentRoom()
    {
        if (_isQTE)
        {
            _player.SetActive(true);

            _playerCamera.gameObject.SetActive(true);

            _player.transform.position = _lastPlayerPosition - Vector3.forward;

            //if(RoomGenerator._currentRooms.Count > 2)
            //{
            //    _player.transform.position = RoomGenerator._currentRooms[1].transform.Find("PlayerSpawnPosition").position;
            //}
            //else
            //{
            //    _player.transform.position = RoomGenerator._currentRooms[0].transform.Find("PlayerSpawnPosition").position;
            //}
            
            yield return new WaitForSeconds(0.2f);

            _eventBus.OnPlayerTeleportedFinish?.Invoke();
        }

        yield return null;

        _isQTE = false;
    }
    public IEnumerator TeleportToQTE()
    {
        if (!_isQTE)
        {
            _lastPlayerPosition = _player.transform.position;

            _player.SetActive(false);

            _playerCamera.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.2f);

            _eventBus.OnPlayerTeleportedFinish?.Invoke();

        }

        yield return null;

        _isQTE = true;
    }

    public IEnumerator TeleportToMainGame()
    {
        if (_isQTE)
        {
            _player.SetActive(true);

            _playerCamera.gameObject.SetActive(true);

            _player.transform.position = RoomGenerator._currentRooms[1].transform.Find("PlayerSpawnPosition").position;

            yield return new WaitForSeconds(0.2f);

            _eventBus.OnPlayerTeleportedFinish?.Invoke();
        }

        yield return null;

        _isQTE = false;
    }

    private void OnDestroy()
    {
        _eventBus.OnFadeActivated -= TeleporterToQTE;
        _eventBus.OnFadeActivated -= TeleporterToMainGame;

        _eventBus.OnLoseFadeActivated -= TeleporterToCurrentRoom;
    }
}
