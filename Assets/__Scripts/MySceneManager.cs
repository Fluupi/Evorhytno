using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    #region Singleton

    private static MySceneManager instance;
    public static MySceneManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    #endregion

    [SerializeField]
    private SceneReference currentScene;
    [Space]
    [SerializeField]
    private SceneReference titleScreen;
    [SerializeField]
    private SceneReference gameScene;
    [SerializeField]
    private SceneReference endScreen;

    [HideInInspector]
    public bool IsWin;

    public void LoadTitle()
    {
        currentScene = titleScreen;
        SceneManager.LoadScene(currentScene);
    }

    public void LoadGame()
    {
        currentScene = gameScene;
        SceneManager.LoadScene(currentScene);
    }

    public void LoadEnd(bool win)
    {
        IsWin = win;
        currentScene = endScreen;
        SceneManager.LoadScene(currentScene);
    }
}
