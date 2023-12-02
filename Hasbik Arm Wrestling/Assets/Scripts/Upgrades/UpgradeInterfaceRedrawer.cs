using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeInterfaceRedrawer : MonoBehaviour
{
    [SerializeField]
    private Button _buyButton;
    [SerializeField]
    private TextMeshProUGUI _upgradeNameText;
    [SerializeField]
    private TextMeshProUGUI _upgradeDescriptionText;
    [SerializeField]
    private TextMeshProUGUI _upgradeLevelText;
    [SerializeField]
    private TextMeshProUGUI _upgradeCostText;

    public void SetUpgradeName(string UpgradeName)
    {
        _upgradeNameText.text = UpgradeName;
    }
    public void RedrawUpgardeInterface(string UpgardeDescription, string UpgradeLevel, string UpgardeCost)
    {
        _upgradeLevelText.text = UpgradeLevel;

        _upgradeDescriptionText.text = UpgardeDescription;

        _upgradeCostText.text = UpgardeCost;
    }

    public void BuyButtonActivator(bool isButtonActive)
    {
        _buyButton.interactable = isButtonActive;
    }
}
