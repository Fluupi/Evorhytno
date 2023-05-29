using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
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
        if (Input.GetButtonDown("No"))
        {
            MySceneManager.Instance.GameMode = Mode.Procedural;
            StartGame();
        }
        /*if (Input.GetButtonDown("Ce"))
        {
            MySceneManager.Instance.GameMode = Mode.SimpleEndless;
            StartGame();
        }*/
        /*if (Input.GetButtonDown("Ros"))
        {
            MySceneManager.Instance.GameMode = Mode.ChaoticEndless;
            StartGame();
        }*/
        if (Input.GetButtonDown("Rhi") || Input.anyKeyDown)
        {
            MySceneManager.Instance.GameMode = Mode.Scripted;
            StartGame();
        }
    }

    private Tween FadeOut()
    {
        return _backgroundImage.DOColor(_fadeOutColor, _fadeOutDuration).SetEase(_fadeOutEase);
    }

    private void StartGame()
    {
        var seq = DOTween.Sequence();
        seq.Append(FadeOut());
        seq.AppendInterval(_sceneChangeDelay);
        seq.AppendCallback(MySceneManager.Instance.LoadGame);
    }
}
