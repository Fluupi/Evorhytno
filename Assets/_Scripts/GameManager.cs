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

    [SerializeField] private bool startToggle;
    [SerializeField] private bool pauseToggle;

    [Header("GameMode")]
    [SerializeField] private GameMode currentGameMode;
    [Space]
    [SerializeField] private GameMode baseGameMode;
    [SerializeField] private GameMode infiniteGameMode;

    [Header("Rhino refs")]
    [SerializeField] private GameObject rhinoGroup;
    [SerializeField] private Rhino[] rhinoLives;

    #region GameCycle

    public void LaunchInfinityGame()
    {
        PrepareGame();

        currentGameMode = infiniteGameMode;
        currentGameMode.LaunchGame();
    }

    public void LaunchBaseGame()
    {
        PrepareGame();

        currentGameMode = baseGameMode;
        currentGameMode.LaunchGame();
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

    #endregion

    private void Update()
    {
        if(currentGameMode == null)
            return;

        if(!startToggle || currentGameMode.IsPlaying)
            return;

        startToggle = false;

        LaunchBaseGame();
    }
}
