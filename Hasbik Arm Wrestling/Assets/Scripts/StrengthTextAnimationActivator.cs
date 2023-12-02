using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StrengthTextAnimationActivator : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Transform _textPosition;
    [SerializeField]
    private TextMeshProUGUI _strengthUpText;
    [SerializeField]
    private string _strengthUpPhrase;

    private void Awake()
    {
        _strengthUpText.text = _strengthUpPhrase;
    }

    private void Start()
    {
        _eventBus.OnStrengthUpgraded += AnimationActivator;

        gameObject.SetActive(false);

    }

    private void Update()
    {
        transform.position = _mainCamera.WorldToScreenPoint(_textPosition.position);
    }

    public void AnimationActivator()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _eventBus.OnStrengthUpgraded -= AnimationActivator;
    }
}
