using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleAnimationBehaviour : MonoBehaviour
{
    [SerializeField]
    protected EventBus _eventBus;
    [SerializeField]
    protected TextMeshProUGUI _collectibleCountText; 

    private Camera _heroCamera;
    private Vector3 _animationPosition;

    public virtual void Start()
    {
        _heroCamera = GameObject.Find("HeroCamera").GetComponent<Camera>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = _heroCamera.WorldToScreenPoint(_animationPosition);
    }

    public void GetAnimationPosition(Vector3 animationPosition)
    {
        _animationPosition = animationPosition;

        transform.position = _heroCamera.WorldToScreenPoint(_animationPosition);

        gameObject.SetActive(true);
    }

    public void SetCollectibleCount(int collectibleCount)
    {
        _collectibleCountText.text = $"{collectibleCount}";
    }
}
