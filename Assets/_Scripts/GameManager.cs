using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField]
    private SceneReference _endScreenWin;

    [SerializeField]
    private SceneReference _endScreenLose;

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

    [Header("Debug")]
    [SerializeField] private bool startToggle;
    [SerializeField] private bool pauseToggle;
    [SerializeField] private bool isPaused;

    [Header("Refs")]
    [SerializeField] private GameMode currentGameMode;
    [Space]
    [SerializeField] private GameMode baseGameMode;
    [Space] public BtnValue[] CutDino = { BtnValue.Rhi, BtnValue.No, BtnValue.Ce, BtnValue.Ros };
    public BiomeData Data;
    public BiOption CurrentOption;

    [Header("Rhino refs")]
    [SerializeField] private GameObject rhinoGroup;
    [SerializeField] private Rhino[] rhinoLives;

    public Biome CurrentBiome;

    #region GameCycle
    public void PrepareGame()
    {
        rhinoGroup.SetActive(true);

        foreach (Rhino life in rhinoLives)
            life.SetAlive(true);
    }

    public void LaunchBaseGame()
    {
        PrepareGame();

        currentGameMode = baseGameMode;
        currentGameMode.LaunchGame();
    }
    

    public void LooseLifePoint()
    {
        int i = 0;

        while (!rhinoLives[i].IsAlive)
        {
            if (i < rhinoLives.Length)
            {
                LooseGame();
                return;
            }
            
            i++;
        }

        rhinoLives[i].SetAlive(false);

        if (i == rhinoLives.Length-1)
            LooseGame();
    }

    private void LooseGame()
    {
        currentGameMode.Stop();
        Debug.Log("You Lost");
        SceneManager.LoadScene(_endScreenLose);
    }

    public void Win()
    {
        currentGameMode.Stop();
        Debug.Log("You Won");
        SceneManager.LoadScene(_endScreenWin);
    }

    #endregion

    public void UpdateData(Biome biome)
    {
        if(CurrentOption.Scene != null)
            CurrentOption.Scene.SetActive(false);

        CurrentBiome = biome;
        Data.SwitchOption();

        switch (CurrentBiome)
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

        if(CurrentOption.Scene == null)
            Debug.LogError("wtf");
        else
            CurrentOption.Scene.SetActive(true);

        foreach (var rhino in rhinoLives)
            rhino.LoadData(CurrentOption.RhinoScale, CurrentOption.RhinoMat);

        currentGameMode.UpdateData(CurrentOption);
    }

    public void NextStep()
    {
        currentGameMode.NextStep();
    }

    private void Start()
    {
        LaunchBaseGame();
    }

    private void Update()
    {
        /*if (!startToggle && !pauseToggle)
            return;

        if (startToggle)
        {
            startToggle = false;
            if (currentGameMode.IsPlaying)
                return;

            LaunchBaseGame();
        }
        
        if (pauseToggle)
        {
            if(currentGameMode == null)
                return;

            pauseToggle = false;

            PauseToggle();
        }*/
    }
}
