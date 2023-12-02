using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StrengthCountRedrawer : MonoBehaviour
{
    [SerializeField]
    private Image _strengthBar;
    [SerializeField]
    private TextMeshProUGUI _strengthCountText;
    [SerializeField]
    private TextMeshProUGUI _strengthLevelText;

    public void RedrawStrengthCount(float currentStrengthCount, float strengthForNextLevel, int strengthLevel)
    {
        _strengthBar.fillAmount = currentStrengthCount / strengthForNextLevel;

        _strengthCountText.text = $"{currentStrengthCount} / {strengthForNextLevel}";

        _strengthLevelText.text = $"{strengthLevel}";
    }
}
