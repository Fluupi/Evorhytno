using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class GameMode : MonoBehaviour
{
    [Header("GameMode")]
    [SerializeField] protected AudioManager audioManager;
    [SerializeField] protected int currentLevel;
    [SerializeField] protected bool isPlaying;

    protected int progress;

    public void LaunchGame()
    {
        currentLevel = -1;
        LaunchNextLevel();
    }

    public abstract void LaunchNextLevel();

    public void Stop()
    {
        isPlaying = false;
        audioManager.Stop();
    }

    public void Resume()
    {
        isPlaying = false;
    }

    public abstract Biome GetBiome();

    public abstract ProcessedPartition GetPartition();

    public void PlayStep()
    {
        audioManager.PlayScheduled(GetPartition());
    }

    public void NextStep()
    {
        if(++progress < 4)
            PlayStep();
        else
            LaunchNextLevel();
    }
}

public enum Biome
{
    Foret,   //tapir
    Steppe,  //geant
    Riviere, //trapu
    Glaciere //laineux
}