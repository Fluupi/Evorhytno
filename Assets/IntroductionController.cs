using System;
using UnityEngine;
using DG.Tweening;
using SGJGrenoble2023;
using UnityEngine.SceneManagement;

public class IntroductionController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup[] _texts;

    [SerializeField]
    private SceneReference _gameScene;

    private int _currentIndex = -1;

    private void Awake()
    {
        foreach (var text in _texts) {
            text.alpha = 0;
        }
    }

    private void Start()
    {
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