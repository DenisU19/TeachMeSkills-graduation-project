using UnityEngine;
using UnityEngine.UI;

public class ExersicesProgressBarDrawer : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Image _progressBarImage;

    private void Start()
    {
        //_eventBus.OnExerciseExecuting += ProgressBarRedrawer;
        _eventBus.OnExerciseExecuting += ProgressBarRedrawer;
        _eventBus.OnExerciseExecuteEnd += ExecutingBarHide;
        _eventBus.OnExerciseInterrupted += ExecutingBarHide;

        _eventBus.OnExerciseBoostActive += GreenProgressBarColorSet;
        _eventBus.OnExerciseBoostDisactive += WhiteProgressBarColorSet;

        ExecutingBarHide();
    }

    public void ExecutingBarHide()
    {
        gameObject.SetActive(false);
    }

    public void ProgressBarRedrawer(float ProgressCount, Transform exersiceProgressBarPosition)
    {
        if(gameObject.activeInHierarchy == false)
        {
            gameObject.SetActive(true);
        }
        _progressBarImage.fillAmount = ProgressCount;

        transform.position = _mainCamera.WorldToScreenPoint(exersiceProgressBarPosition.position);
    }

    public void GreenProgressBarColorSet()
    {
        _progressBarImage.color = Color.green;

    }

    public void WhiteProgressBarColorSet()
    {
        _progressBarImage.color = Color.white;
    }

    private void OnDestroy()
    {
        _eventBus.OnExerciseExecuting -= ProgressBarRedrawer;
        _eventBus.OnExerciseExecuteEnd -= ExecutingBarHide;
        _eventBus.OnExerciseInterrupted -= ExecutingBarHide;

        _eventBus.OnExerciseBoostActive -= GreenProgressBarColorSet;
        _eventBus.OnExerciseBoostDisactive -= WhiteProgressBarColorSet;
    }
}
