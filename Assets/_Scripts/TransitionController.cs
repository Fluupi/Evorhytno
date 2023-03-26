using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TransitionController : MonoBehaviour
{
    [SerializeField]
    private AudioClip _transitionClip;

    [SerializeField]
    private AudioSource _sfxSource;
    
    private Animator _animator;

    private readonly int _trigger = Animator.StringToHash("Play");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayTransition()
    {
        _animator.SetTrigger(_trigger);
        _sfxSource.PlayOneShot(_transitionClip);
    }
}
