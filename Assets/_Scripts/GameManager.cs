using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            i++;

        rhinoLives[i].SetAlive(false);

        if (i == rhinoLives.Length-1)
            LooseGame();
    }

    private void LooseGame()
    {
        currentGameMode.Stop();
        Debug.Log("You Lost");
    }

    public void Win()
    {
        currentGameMode.Stop();
        Debug.Log("You Won");
    }

    #endregion

    public void UpdateData(BiOption data)
    {
        currentGameMode.UpdateData(data);

        CurrentOption.Scene.SetActive(false);
        CurrentOption = data;
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
