using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class UIVFXController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _vfxHit;
    
    [SerializeField]
    private ParticleSystem _vfxMissed;

    [Space]

    [SerializeField]
    private float _wordFadeInDuration;

    [SerializeField]
    private Ease _wordFadeInEase;

    [SerializeField]
    private float _wordFadeOutDuration;

    [SerializeField]
    private Ease _wordFadeOutEase;

    [SerializeField]
    private float _wordDuration = 2;
    
    [SerializeField]
    private Image _imageRhi;

    [SerializeField]
    private Image _imageNo;

    [SerializeField]
    private Image _imageCe;

    [SerializeField]
    private Image _imageRos;

    private Tween _tween;
    
    [ContextMenu("Hit")]
    private void TestHit()
    {
        PlayVFXHit(BtnValue.Ce);
    }
    
    public void PlayVFXHit(BtnValue button)
    {
        _vfxHit.Play();

        var image = button switch {
            BtnValue.Rhi => _imageRhi,
            BtnValue.No => _imageNo,
            BtnValue.Ce => _imageCe,
            BtnValue.Ros => _imageRos
        };

        var rect = image.rectTransform;
        
        _tween?.Kill(true);
        
        var seq = DOTween.Sequence();
        seq.Append(image.DOFade(1, _wordFadeInDuration).SetEase(_wordFadeInEase));
        seq.Append(rect.DOScale(1.5f, _wordDuration).SetEase(Ease.OutCirc));
        seq.Join(rect.DOLocalMove(new Vector3(Random.Range(-50, 50), Random.Range(50,100), 0), _wordDuration).SetEase(Ease.OutCirc));
        seq.Append(image.DOFade(0, _wordFadeOutDuration).SetEase(_wordFadeOutEase));
        seq.Play().OnComplete(() => {
            rect.localScale = Vector3.one;
            rect.localPosition = Vector3.zero;
        }); 
        _tween = seq;
    }
    
    private IEnumerator WaitForParticleSystemEnd(ParticleSystem particleSystem, Action callback = null)
    {
        yield return new WaitWhile(() => particleSystem.isPlaying);
        callback?.Invoke();
    }

    [ContextMenu("Missed")]
    public void PlayVFXMissed()
    {
        _vfxMissed.Play();
    }
}
