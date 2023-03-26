using UnityEngine;

using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    [SerializeField]
    private SceneReference _startScene;
    
    [Space]
    
    [SerializeField]
    private Image _backgroundImage;
    
    [SerializeField]
    private Color _fadeOutColor = Color.black;

    [SerializeField]
    private float _fadeOutDuration = 3f;

    [SerializeField]
    private Ease _fadeOutEase = Ease.OutCubic;

    [SerializeField]
    private float _sceneChangeDelay = 1f;

    
    
    private void Update()
    {
        if (Input.anyKeyDown ||
            Input.GetButtonDown("Rhi") ||
            Input.GetButtonDown("No") ||
            Input.GetButtonDown("Ce") ||
            Input.GetButtonDown("Ros")) {
            ReloadGame();
        }
    }

    private Tween FadeOut()
    {
        return _backgroundImage.DOColor(_fadeOutColor, _fadeOutDuration).SetEase(_fadeOutEase);
    }

    private void ReloadGame()
    {
        var seq = DOTween.Sequence();
        seq.Append(FadeOut());
        seq.AppendInterval(_sceneChangeDelay);
        seq.AppendCallback(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_startScene);
    }
}
