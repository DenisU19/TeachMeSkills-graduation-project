using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinShopRedrawer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _skinNameText;
    [SerializeField]
    private TextMeshProUGUI _skinDescriptionText;
    [SerializeField]
    private TextMeshProUGUI _skinBuyButtonText;
    [SerializeField]
    private TextMeshProUGUI _skinSelectButtonText;

    public void DrawSkinMainCharacterstics(string skinName, string skinDescription)
    {
        _skinNameText.text = $"{skinName}";
        _skinDescriptionText.text = $"{skinDescription}";
    }

    public void RedrawSkinBuyButtonText(string skinCost)
    {
        _skinBuyButtonText.text = skinCost;
    }
    public void RedrawSkinSelectButtonText(string buttonText)
    {
        _skinSelectButtonText.text = buttonText;
    } 
}
