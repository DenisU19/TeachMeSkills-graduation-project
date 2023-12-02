using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    private EventBus _eventBus;

    public static bool _isJoystickTouch;
    public void OnPointerDown(PointerEventData eventData)
    {
        _eventBus.OnJoystickTouchedStart?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isJoystickTouch = false;

        _eventBus.OnJoystickTouchEnd?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _isJoystickTouch = true;
    }
}
