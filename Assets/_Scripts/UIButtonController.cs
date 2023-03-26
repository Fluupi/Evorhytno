using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour
{
    [SerializeField]
    private Image _sprite;
    
    [SerializeField]
    private Image _timerSprite;

    [SerializeField]
    private float _timerSpriteMaxScale = 5;

    [Space]

    [SerializeField]
    private ParticleSystem _vfxGood;

    [SerializeField]
    private ParticleSystem _vfxBad;

    private RectTransform _timerSpriteRect;
    private CanvasGroup _timerSpriteGroup;

    private void Awake()
    {
        _timerSpriteRect = _timerSprite.GetComponent<RectTransform>();
        _timerSpriteGroup = _timerSprite.GetComponent<CanvasGroup>();
        _timerSpriteGroup.alpha = 0;
        _timerSpriteRect.localScale = new Vector3(_timerSpriteMaxScale, _timerSpriteMaxScale, _timerSpriteMaxScale);
    }

    public void PlayAnimation(float duration)
    {
        _timerSpriteGroup.DOFade(1, .1f).SetEase(Ease.OutCubic);
        _timerSpriteRect.DOScale(1, duration).SetEase(Ease.Linear).OnComplete(() => {
            _timerSpriteGroup.alpha = 0;
            _timerSpriteRect.localScale = new Vector3(_timerSpriteMaxScale, _timerSpriteMaxScale, _timerSpriteMaxScale);
        });
    }

    public void PlayVFXGood()
    {
        _vfxGood.Play();
    }

    public void PlayVFXBad()
    {
        _vfxBad.Play();
    }

    [ContextMenu("Test")]
    private void Test()
    {
        PlayAnimation(2);
    }
}
