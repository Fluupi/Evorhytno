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
}

public enum Biome
{
    Foret,   //tapir
    Steppe,  //geant
    Riviere, //trapu
    Glaciere //laineux
}