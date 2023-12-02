using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerTapDrawer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Image _playerTapImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(RedrawPlayerTap(eventData.position));
    }

    public IEnumerator RedrawPlayerTap(Vector3 tapPosition)
    {
        _playerTapImage.transform.position = tapPosition;

        _playerTapImage.gameObject.SetActive(true);

        yield return null;

        _playerTapImage.gameObject.SetActive(false);
    }


}
