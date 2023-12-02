using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExerciseRewardAnimationBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _goldValue;
    [SerializeField]
    private TextMeshProUGUI _strengthValue;

    private Camera _playerCamera;
    private Vector3 _animationPosition;

    private void Start()
    {
        _playerCamera = GameObject.Find("HeroCamera").GetComponent<Camera>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = _playerCamera.WorldToScreenPoint(_animationPosition);
    }

    public void GetCurrentReward(Vector3 animationPosition, float goldValue, float strengthValue)
    {
        _animationPosition = animationPosition;
        _goldValue.text = $"{goldValue}";
        _strengthValue.text = $"{strengthValue}";

        gameObject.SetActive(true);
    }
}
