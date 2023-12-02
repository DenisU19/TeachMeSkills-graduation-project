using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SkinShopRedrawer))]
public class SkinShopBehaviour : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private SkinData _currentShopSkin;
    [SerializeField]
    private Button _skinBuyButton;
    [SerializeField]
    private Button _skinSelectButton;


    private SkinShopRedrawer _skinShopRedrawer;

    private void Awake()
    {
        _skinShopRedrawer = GetComponent<SkinShopRedrawer>();

        _skinShopRedrawer.DrawSkinMainCharacterstics(_currentShopSkin.SkinName, _currentShopSkin.SkinDescription);
    }

    private void OnEnable()
    {
        SelectShopButtons();
    }

    private void Start()
    {
        _eventBus.OnCurrentSkinChanged += BuyButtonActivate;
        _eventBus.OnNewSkinBought += CheckBuyPossibility;
    }

    public void SelectShopButtons()
    {
        if (_currentShopSkin._isSkinBuy)
        {
            _skinBuyButton.gameObject.SetActive(false);

            _skinSelectButton.gameObject.SetActive(true);

            if (_currentShopSkin._isSkinActive)
            {
                _skinShopRedrawer.RedrawSkinSelectButtonText("Выбрано");

                _skinSelectButton.interactable = false;
            }
            else
            {
                _skinShopRedrawer.RedrawSkinSelectButtonText("Выбрать");

                _skinSelectButton.interactable = true;
            }
        }
        else
        {
            _skinSelectButton.gameObject.SetActive(false);

            _skinBuyButton.gameObject.SetActive(true);
        }

        CheckBuyPossibility();
    }

    public void CheckBuyPossibility()
    {
        if (_currentShopSkin._isSkinBuy == false)
        {
            if (MoneyCounter.GoldCount >= _currentShopSkin.SkinPrice)
            {
                _skinBuyButton.interactable = true;
            }
            else
            {
                _skinBuyButton.interactable = false;
            }

            _skinShopRedrawer.RedrawSkinBuyButtonText(_currentShopSkin.SkinPrice.ToString());
        }
    }

    public void BuySkin()
    {
        _eventBus.OnMoneySpended?.Invoke(_currentShopSkin.SkinPrice);

        _currentShopSkin.ChangeBuyStatus();

        _eventBus.OnNewSkinBought?.Invoke();

        SelectShopButtons();
    }

    public void SelectSkin()
    {
        _eventBus.OnCurrentSkinChanged?.Invoke(_currentShopSkin);

        BuyButtonActivate(_currentShopSkin);

        _skinBuyButton.interactable = false;

        SelectShopButtons();
    }

    public void BuyButtonActivate(SkinData currentSkin)
    {
        if(_currentShopSkin == currentSkin && _currentShopSkin._isSkinBuy && _currentShopSkin._isSkinActive)
        {
            _skinBuyButton.interactable = false;
        }
        else
        {
            _skinBuyButton.interactable = true;
        }

        SelectShopButtons();
    }

    private void OnDestroy()
    {
        _eventBus.OnCurrentSkinChanged -= BuyButtonActivate;
        _eventBus.OnNewSkinBought -= CheckBuyPossibility;
    }
}
