using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArmwrestlingBarRedrawer : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private RectTransform _progressBarTransform;
    [SerializeField]
    private RectTransform _separator;
    [SerializeField]
    private TextMeshProUGUI _armwrestlingRewardText;

    private Vector3 _startSeparatorPosition;
    private Image _progressBarImage;
    private float _progresBarWidth;


    private void Awake()
    {
        _progresBarWidth = _progressBarTransform.rect.width;

        _progressBarImage = _progressBarTransform.gameObject.GetComponent<Image>();

        _startSeparatorPosition = _separator.position;
    }

    private void OnEnable()
    {
        BarRestartRedraw();
    }

    private void Start()
    {
        _eventBus.OnPlayerTaped += RedrawPlayerTap;
        _eventBus.OnArmwrestlingEnemyActive += DecreaseProgressBar;
    }

    public void BarRestartRedraw()
    {
        _separator.position = _startSeparatorPosition;

        _progressBarImage.fillAmount = 0.5f;

        //_separator.transform.position = new Vector3(_separator.transform.position.x - _progresBarWidth / 2, _separator.transform.position.y, _separator.transform.position.z);
        _separator.transform.position = new Vector3(_startSeparatorPosition.x, _startSeparatorPosition.y, _separator.transform.position.z);
    }

    public void RedrawPlayerTap(float tapForce)
    {
        _progressBarImage.fillAmount += tapForce;

        if (_progressBarImage.fillAmount < 1)
        {
            _separator.position = new Vector3(_separator.transform.position.x + (_progresBarWidth * tapForce), _separator.transform.position.y, _separator.transform.position.z);
        }
        else
        {
            _progressBarImage.fillAmount = 1f;

            //_separator.position = _startSeparatorPosition;
            _separator.transform.localPosition = new Vector3(_progresBarWidth , _separator.transform.localPosition.y, _separator.transform.localPosition.z);

            _eventBus.OnQTECompleted?.Invoke();
        }
    }

    public void DecreaseProgressBar(float enemyStrength)
    {
        if (_progressBarImage.fillAmount > 0)
        {
            _progressBarImage.fillAmount -= enemyStrength;

            _separator.transform.localPosition = new Vector3(_progresBarWidth * _progressBarImage.fillAmount, _separator.transform.localPosition.y, _separator.transform.localPosition.z);

            //_separator.position = new Vector3(_separator.transform.position.x - (_progresBarWidth * enemyStrength), _separator.transform.position.y, _separator.transform.position.z);
            //_separator.position = new Vector3(_separator.transform.position.x - (_progresBarWidth * enemyStrength), _separator.transform.position.y, _separator.transform.position.z);
            //_separator.position = new Vector3(/*_separator.transform.position.x*/ - (_progresBarWidth * _progressBarImage.fillAmount), _separator.transform.position.y, _separator.transform.position.z);
        }
        else
        {
            _eventBus.OnQTEFailed.Invoke();
        }
    }

    public void DrawArmwrestlingReward(int reward)
    {
        _armwrestlingRewardText.text = $"{reward}";
    }

    private void OnDestroy()
    {
        _eventBus.OnPlayerTaped -= RedrawPlayerTap;
        _eventBus.OnArmwrestlingEnemyActive -= DecreaseProgressBar;
    }
}
