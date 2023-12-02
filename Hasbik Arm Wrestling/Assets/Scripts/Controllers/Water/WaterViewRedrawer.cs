using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaterViewRedrawer : MonoBehaviour
{
    [SerializeField]
    private Image _waterBar;
    [SerializeField]
    private TextMeshProUGUI _waterPercentText;

    public void RedrawWaterCount(float currentWaterCount, float maxWaterCount)
    {
        _waterBar.fillAmount = currentWaterCount / maxWaterCount;

        _waterPercentText.text = $"{Mathf.RoundToInt(currentWaterCount / maxWaterCount * 100)}%";
    }

}
    
