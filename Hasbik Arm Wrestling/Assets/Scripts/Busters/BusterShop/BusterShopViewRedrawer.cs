using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BusterShopViewRedrawer : MonoBehaviour
{
    [SerializeField]
    private Button _busterBuyButton;
    [SerializeField]
    private TextMeshProUGUI _busterNameText;
    [SerializeField]
    private TextMeshProUGUI _busterDescriptionText;
    [SerializeField]
    private TextMeshProUGUI _busterCostText;

    public void DrawBusterData(string busterName, string busterDescription, string busterCost)
    {
        _busterNameText.text = busterName;
        _busterDescriptionText.text = busterDescription;
        _busterCostText.text = busterCost;
    }

    public void DrawBuyPossibility(bool _isBuyButtonActive)
    {
        _busterBuyButton.interactable = _isBuyButtonActive;
    }
}
