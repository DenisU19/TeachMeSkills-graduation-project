using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BusterViewDrawer : MonoBehaviour
{
    [SerializeField]
    private Button _busterButton;
    [SerializeField]
    private Image _busterImage;
    [SerializeField]
    private TextMeshProUGUI _busterCountText;

    private bool _isReloading;

    public void RedrawBusterView(int currentBusterCount)
    {
        _busterCountText.text = $"x{currentBusterCount}";

        if(_busterButton.interactable == false && currentBusterCount > 0 && _isReloading == false)
        {
            _busterButton.interactable = true;
        }
        else if(currentBusterCount == 0)
        {
            _busterButton.interactable = false;
        }
    }

    public void BusterButtonActivate(bool isButtonActive)
    {
        _busterButton.interactable = isButtonActive;
    }

    public IEnumerator BusterReloading(float reloadTime, int currentBusterCount)
    {
        _busterCountText.text = $"x{currentBusterCount}";

        _busterImage.fillAmount = 0;

        BusterButtonActivate(false);

        _isReloading = true;

        while (_busterImage.fillAmount < 1)
        {
            _busterImage.fillAmount += Mathf.Lerp(0f, 1f,  1 / reloadTime * Time.deltaTime);

            yield return null;
        }

        if (currentBusterCount > 0)
        {
            BusterButtonActivate(true);
        }

        _isReloading = true;
    }
}
