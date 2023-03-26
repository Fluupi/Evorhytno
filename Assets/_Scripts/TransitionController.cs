using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TransitionController : MonoBehaviour
{
    private Animator _animator;

    private readonly int _trigger = Animator.StringToHash("Play");

    public void PlayTransition()
    {
        _animator.SetTrigger(_trigger);
    }
}
