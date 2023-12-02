using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaterAdderViewRedrawer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _waterPriceText;
    [SerializeField]
    private TextMeshProUGUI _waterButtonText;
    [SerializeField]
    private Button _addWaterButton;
    [SerializeField]
    private Image _coinImage;
    [SerializeField]
    private string _fullWaterButtonInscription; 
    [SerializeField]
    private string _emptyWaterButtonInscription;

    public void RedrawView(int waterPrice, bool isBuyButtonActive)
    {
        _waterPriceText.text = $"{waterPrice}";

        _waterButtonText.text = _emptyWaterButtonInscription;

        _coinImage.enabled = true;

        _waterPriceText.enabled = true;

        if (isBuyButtonActive)
        {
            _addWaterButton.interactable = true;
        }
        else
        {
            _addWaterButton.interactable = false;
        }
    }

    public void DrawFullWaterCount()
    {
        _coinImage.enabled = false;

        _waterPriceText.enabled = false;

        _waterButtonText.text = _fullWaterButtonInscription;

        _addWaterButton.interactable = false;

    }
}
