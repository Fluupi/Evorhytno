using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class GameMode : MonoBehaviour
{
    [SerializeField] protected AudioManager audioManager;
    [SerializeField] protected int currentLevel;
    [SerializeField] protected Partition partition;
    [SerializeField] protected float gameSpeed;
    [SerializeField] protected bool isPlaying;

    protected int progress;

    public bool IsPlaying => isPlaying;

    public abstract void ChooseBiome();

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

    public void UpdateData(BiOption data)
    {
        AmbiantMusicController.Instance.PlayAmbiant(data.AmbiantAudioClip);
    }

    public void PlayStep()
    {
        audioManager.PlayScheduled(partition.GenerateRandomScript());
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