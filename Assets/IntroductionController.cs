using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class IntroductionController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup[] _texts;

    [SerializeField]
    private SceneReference _gameScene;

    [SerializeField]
    private AudioClip _introClip;

    private int _currentIndex = -1;

    private void Awake()
    {
        foreach (var text in _texts) {
            text.alpha = 0;
        }
    }

    private void Start()
    {
        AmbiantMusicController.Instance.PlayAmbiant(_introClip);
        NextText();
    }

    private void Update()
    {
        if (Input.anyKeyDown ||
            Input.GetButtonDown("Rhi") ||
            Input.GetButtonDown("No") ||
            Input.GetButtonDown("Ce") ||
            Input.GetButtonDown("Ros")) {
            NextText();
        }
    }

    private void NextText()
    {
        if (_currentIndex > -1) {
            FadeOut(_texts[_currentIndex]);
        }

        _currentIndex++;

        if (_currentIndex >= _texts.Length) {
            LoadGameScene();
            return;
        }
        FadeIn(_texts[_currentIndex]);
    }

    private void FadeOut(CanvasGroup text)
    {
        text.DOKill();
        text.DOFade(0, .2f).SetEase(Ease.OutCubic);
    }

    private void FadeIn(CanvasGroup text)
    {
        text.DOKill();
        text.DOFade(1, 1f).SetEase(Ease.OutCubic);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(_gameScene);
    }
}
