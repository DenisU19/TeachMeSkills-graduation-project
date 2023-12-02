using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private LoadingBarRedrawer _loadingBarRedrawer;
    [SerializeField]
    private float _sceneLoadingSpeed; // скорость загрузки сцены
    [SerializeField]
    private string _loadingSceneName; // Имя сцены, которую хотим загрузить

    private AsyncOperation _asyncOperation;
    private float _currentLoadingProgress;
    private bool _isStartSceneSwitch;

    private void Awake()
    {
        _eventBus.OnPlayerDataLoaded += SwitchScene;
    }

    private void Update()
    {
        _currentLoadingProgress += _sceneLoadingSpeed * Time.deltaTime;

        _loadingBarRedrawer.DrawLoadingProgress(_currentLoadingProgress);

        if(_currentLoadingProgress >= 0.9 && _isStartSceneSwitch == false)
        {
            _isStartSceneSwitch = true;

            _asyncOperation = SceneManager.LoadSceneAsync(_loadingSceneName, LoadSceneMode.Additive);
        }

        SwitchScene();
    }

    public void SwitchScene()
    {
        if (_isStartSceneSwitch == true && _asyncOperation.isDone)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_loadingSceneName));

            SceneManager.UnloadSceneAsync("Loading");
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnPlayerDataLoaded -= SwitchScene;
    }
}
