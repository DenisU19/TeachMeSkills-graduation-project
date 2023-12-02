using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private PlayerStats _playerStats;
    [SerializeField]
    private WaterCountSettings _waterCountSettings;
    [SerializeField]
    private BusterManager _busterManager;
    [SerializeField]
    private Joystick _joystick;

    private Rigidbody _rigidbody;
    private float _movementSpeed;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _movementSpeed = _playerStats.AllStats[PlayerStats.HeroStats.MovementSpeed];

        _eventBus.OnRoomCompleted += MovementDisactivate;
        _eventBus.OnPlayerTeleportedFinish += MovementActivate;
    }

    private void FixedUpdate()
    {
        _moveDirection = new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y);
        if (WaterCounter.ÑurrentWaterCount > 0)
        {
            _rigidbody.MovePosition(transform.position + _moveDirection * _movementSpeed * _busterManager.CurrentBusterEffectValue * Time.deltaTime);

        }
        else
        {
            _rigidbody.MovePosition((transform.position + _moveDirection * _movementSpeed * _busterManager.CurrentBusterEffectValue * (1 - (_waterCountSettings.AllWaterInfluenceParts[WaterCountSettings.GamePartInfluence.PlayerMovementSpeed] / 100)) * Time.deltaTime));

        }
    }

    public void MovementActivate()
    {
        enabled = true;
    }

    public void MovementDisactivate()
    {
        enabled = false;
    }

    private void OnDestroy()
    {
        _eventBus.OnRoomCompleted -= MovementDisactivate;
        _eventBus.OnPlayerTeleportedFinish -= MovementActivate;
    }
}
