using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExerciseQTEButton : MonoBehaviour, IPointerEnterHandler, /*IPointerExitHandler,*/ IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private GameObject _nextButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        _eventBus.OnPlayerStartTrace?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _eventBus.OnPlayerStartTrace?.Invoke();
        _eventBus.OnPlayerTraceFigure?.Invoke(gameObject, _nextButton, Time.realtimeSinceStartup);
    }

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    _eventBus.OnPlayerDisactive?.Invoke();
    //}

    public void OnPointerUp(PointerEventData eventData)
    {
        _eventBus.OnPlayerStopTrace?.Invoke();
    }
}
