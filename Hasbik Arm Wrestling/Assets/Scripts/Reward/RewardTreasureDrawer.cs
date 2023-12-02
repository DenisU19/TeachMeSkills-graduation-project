using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardTreasureDrawer : MonoBehaviour
{
    [SerializeField]
    private Image _treasureTimeBar;
    [SerializeField]
    private float _treasureActiveTime;

    private float _currentTime;

    private void OnEnable()
    {
        _currentTime = _treasureActiveTime;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;

        if(_currentTime <= 0f)
        {
            _currentTime = 0f;
            gameObject.SetActive(false);
        }


        _treasureTimeBar.fillAmount = _currentTime / _treasureActiveTime;
    }

}
