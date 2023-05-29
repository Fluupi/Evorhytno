using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }
    #endregion

    public UnityEvent OnForetAppear;
    public UnityEvent OnSteppeAppear;
    public UnityEvent OnRiviereAppear;
    public UnityEvent OnGlacierAppear;

    [Header("Game Mode")]
    public Mode TestMode;
    public GameMode CurrentGameMode;
    [SerializeField] private GameMode scriptedGameMode;
    [SerializeField] private GameMode proceduralGameMode;
    [SerializeField] private GameMode testGameMode;

    [Header("Biome Data")]
    [SerializeField] private BiomeDataSO[] biomeDataSO;
    [SerializeField] private GameObject foretGO;
    [SerializeField] private GameObject steppeGO;
    [SerializeField] private GameObject riviereGO;
    [SerializeField] private GameObject glacierGO;

    [Header("Rhino")]
    [SerializeField] private GameObject rhinoGroup;
    [SerializeField] private Rhino[] rhinoLives;

    public Biome CurrentBiome => CurrentGameMode.GetBiome();

    #region GameCycle
    public void LaunchGame()
    {
        PrepareGame();

        //CurrentGameMode = scriptedGameMode;
        //CurrentGameMode = proceduralGameMode;
        CurrentGameMode = TestMode switch
        {
            Mode.Scripted => scriptedGameMode,
            Mode.Procedural => proceduralGameMode,
            Mode.Test => testGameMode
        };

        CurrentGameMode.LaunchGame();
    }

    public void PrepareGame()
    {
        rhinoGroup.SetActive(true);

        foreach (Rhino life in rhinoLives)
            life.SetAlive(true);
    }

    public void LooseLifePoint()
    {
        int i = 0;

        while (i < rhinoLives.Length && !rhinoLives[i].IsAlive)
            i++;

        if (i+1 >= rhinoLives.Length)
        {
            LooseGame();
            return;
        }

        rhinoLives[i].SetAlive(false);
    }

    private void LooseGame()
    {
        CurrentGameMode.Stop();
        Debug.Log("You Lost");
        MySceneManager.Instance.LoadEnd(false);
    }

    public void Win()
    {
        CurrentGameMode.Stop();
        Debug.Log("You Won");
        MySceneManager.Instance.LoadEnd(true);
    }

    #endregion

    public void PlayTransition(Biome biome)
    {
        switch (biome)
        {
            case Biome.Foret:
                OnForetAppear.Invoke();
                break;
            case Biome.Steppe:
                OnSteppeAppear.Invoke();
                break;
            case Biome.Riviere:
                OnRiviereAppear.Invoke();
                break;
            default:
                OnGlacierAppear.Invoke();
                break;
        }
    }

    public IEnumerator UpdateData(Biome biome)
    {
        foreach (Rhino rhino in rhinoLives)
            rhino.gameObject.SetActive(false);

        yield return new WaitForSeconds(2.5f);

        foreach (Rhino rhino in rhinoLives)
            rhino.gameObject.SetActive(true);

        int i = 0;

        while (biomeDataSO[i].Type != biome)
            i++;

        BiomeDataSO biomeData = biomeDataSO[i];

        switch (biomeData.Type)
        {
            case Biome.Foret:
                foretGO.SetActive(true);
                break;
            case Biome.Steppe:
                steppeGO.SetActive(true);
                break;
            case Biome.Riviere:
                riviereGO.SetActive(true);
                break;
            default:
                glacierGO.SetActive(true);
                break;
        }

        foreach (var rhino in rhinoLives)
            rhino.LoadData(biomeData.RhinoScale, biomeData.RhinoMat);

        AmbiantMusicController.Instance.PlayAmbiant(biomeData.AmbiantAudioClip);
    }

    public void NextStep()
    {
        CurrentGameMode.NextStep();
    }

    private void Start()
    {
        LaunchGame();
    }
}

public enum Mode
{
    Scripted,
    Procedural,
    Test
}