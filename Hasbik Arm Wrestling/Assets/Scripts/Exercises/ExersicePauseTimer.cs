using UnityEngine;
using UnityEngine.UI;


public class ExersicePauseTimer : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private Transform _pauseTimerPosition;
    [SerializeField]
    private Image _pauseTimerImagePrefab;

    private Camera _mainCamera;
    private Transform _pauseTimerParent;
    private Image _pauseTimerImage;
    private float _timerTime;
    private float _currentTime;

    private void Awake()
    {
        _mainCamera = GameObject.Find("HeroCamera").GetComponent<Camera>();

        _pauseTimerParent = GameObject.Find("PauseTimersParent").GetComponent<Transform>();

        _pauseTimerImage = Instantiate(_pauseTimerImagePrefab, transform.position, Quaternion.identity, _pauseTimerParent);

        _pauseTimerImage.gameObject.SetActive(false);

        enabled = false;
    }

    private void Update()
    {
        _pauseTimerImage.transform.position = _mainCamera.WorldToScreenPoint(_pauseTimerPosition.position);

        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            enabled = false;

            _pauseTimerImage.gameObject.SetActive(false);

            _currentTime = 0f;
        }

        _pauseTimerImage.fillAmount = _currentTime / _timerTime;
    }

    public void StartDelayTimerDraw(ExerciseData currentExersice, float delayTime)
    {
        _timerTime = delayTime;

        _currentTime = _timerTime;

        enabled = true;

        _pauseTimerImage.gameObject.SetActive(true);
    }
}
