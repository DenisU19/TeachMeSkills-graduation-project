using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationSwitcher : MonoBehaviour
{
    [SerializeField]
    private Joystick _joystick;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _joystick.Direction.magnitude);
    }

    private void OnDisable()
    {
        _animator.SetFloat("Speed", 0f);
    }
}
