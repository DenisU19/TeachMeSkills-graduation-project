using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BusterTextAnimationBehaviour : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Transform _animationStartPosition;
    [SerializeField]
    private TextMeshProUGUI _bustertextAnimation;
    [SerializeField]
    private string _busterEffectDescription;

    private Transform _animationParent;

    private void Awake()
    {
        _bustertextAnimation.text = _busterEffectDescription;
        _animationParent = transform.parent;
    }

    private void Update()
    {
        _animationParent.transform.position = _mainCamera.WorldToScreenPoint(_animationStartPosition.position);
    }
}
