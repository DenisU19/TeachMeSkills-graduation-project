using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingInterfaceOpener : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    public Button _vendingButton;
    [SerializeField]
    private Transform _buttonPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enabled = true;

            ShowVendingButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enabled = false;

            HideVendingButton();
        }
    }

    //private void Update()
    //{
    //    _vendingButton.transform.position = _mainCamera.WorldToScreenPoint(_buttonPosition.position);
    //}

    public void ShowVendingButton()
    {
        _vendingButton.gameObject.SetActive(true);
    }
    public void HideVendingButton()
    {
        _vendingButton.gameObject.SetActive(false);
    }
}
