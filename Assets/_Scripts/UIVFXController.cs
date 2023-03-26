using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class UIVFXController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _vfxHit;
    
    [SerializeField]
    private ParticleSystem _vfxDeath;
    
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
        
        var seq = DOTween.Sequence();
        seq.Append(image.DOFade(1, _wordFadeInDuration).SetEase(_wordFadeInEase));
        seq.AppendInterval(_wordDuration);
        seq.Append(image.DOFade(0, _wordFadeOutDuration).SetEase(_wordFadeOutEase));
        seq.Play();
    }
    
    private IEnumerator WaitForParticleSystemEnd(ParticleSystem particleSystem, Action callback = null)
    {
        yield return new WaitWhile(() => particleSystem.isPlaying);
        callback?.Invoke();
    }

    [ContextMenu("Death")]
    public void PlayVFXDeath()
    {
        _vfxDeath.Play();
    }

    [ContextMenu("Missed")]
    public void PlayVFXMissed()
    {
        _vfxMissed.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            PlayVFXHit(BtnValue.Ce);
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            PlayVFXDeath();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            PlayVFXMissed();
        }
    }
}
