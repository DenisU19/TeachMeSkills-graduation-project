using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarRedrawer : MonoBehaviour
{
    [SerializeField]
    private SceneSwitcher _sceneSwitcher;
    [SerializeField]
    private Image _loadingProgressBar;

    public void DrawLoadingProgress(float loadingProgress)
    {
        _loadingProgressBar.fillAmount = loadingProgress;
    }

}
