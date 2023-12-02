using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private Joystick _joystick;

    private Vector3 _rotationVector;

    private void Start()
    {
        _eventBus.OnRoomCompleted += RotationDisactivate;
        _eventBus.OnPlayerTeleportedFinish += RotationActivate;

    }

    private void Update()
    {
        PlayerRotate();
    }

    public void RotationActivate()
    {
        enabled = true;
    }

    public void RotationDisactivate()
    {
        enabled = false;
    }

    public void PlayerRotate()
    {
        _rotationVector = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);

        if(_rotationVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_rotationVector);
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnRoomCompleted -= RotationDisactivate;
        _eventBus.OnPlayerTeleportedFinish -= RotationActivate;

    }
}
