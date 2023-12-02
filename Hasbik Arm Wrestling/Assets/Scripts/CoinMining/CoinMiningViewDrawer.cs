using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class CoinMiningViewDrawer : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private Button _unlockeMoneyButton;
    [SerializeField]
    private Button _collectMoneyButton;
    [SerializeField]
    private Transform _buttonsPosition;
    [SerializeField]
    private Image _miningProgessBarImage;

    private Button _currentActiveButton;

    private void Start()
    {
        _eventBus.OnGoldMinerUnlocked += ChangeActiveButton;
        _eventBus.OnGoldMinerUnlocked += ShowMiningProgressBar;
        _eventBus.OnMiningMineyCollected += DrawMiningProgressBar;

        enabled = false;
    }

    public void ChangeActiveButton()
    {
        _currentActiveButton.gameObject.SetActive(false);
        _currentActiveButton = _collectMoneyButton;
    }

    public void ShowMiningProgressBar()
    {
        _miningProgessBarImage.gameObject.SetActive(true);
    }
    public void DrawMiningProgressBar()
    {
        _miningProgessBarImage.fillAmount = CoinMiner.MiningMoneyValue / CoinMiner.MiningCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CoinMinerUnlocker.IsMinerUnlocked)
            {
                _currentActiveButton = _collectMoneyButton;   
            }
            else
            {
                _currentActiveButton = _unlockeMoneyButton;
            }

            _currentActiveButton.gameObject.SetActive(true);
            _currentActiveButton.interactable = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && CoinMinerUnlocker.IsMinerUnlocked)
        {
           if(CoinMiner.MiningMoneyValue >= 1f)
           {
                _currentActiveButton.gameObject.SetActive(true);
           }
           else
           {
                _currentActiveButton.gameObject.SetActive(false);
           }
        }
        else if(other.CompareTag("Player") && !CoinMinerUnlocker.IsMinerUnlocked)
        {
            if(MoneyCounter.GoldCount >= CoinMinerUnlocker.MoneyToUnlocke)
            {
                _currentActiveButton.interactable = true;
            }
            else
            {
                _currentActiveButton.interactable = false;
            }
        }

        //_currentActiveButton.transform.position = _mainCamera.WorldToScreenPoint(_buttonsPosition.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentActiveButton.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnGoldMinerUnlocked -= ChangeActiveButton;
        _eventBus.OnGoldMinerUnlocked -= ShowMiningProgressBar;
        _eventBus.OnMiningMineyCollected -= DrawMiningProgressBar;
    }
}
