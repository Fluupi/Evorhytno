using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class GameMode : MonoBehaviour
{
    [SerializeField] protected int currentLevel;
    [SerializeField] protected PlayableDirector director;
    [SerializeField] protected Partition partition;
    [SerializeField] protected float gameSpeed;
    [SerializeField] protected bool isPlaying;
    public bool IsPlaying => isPlaying;
    public void LaunchGame()
    {
        currentLevel = -1;
        LaunchNextLevel();
    }

    public virtual void LaunchNextLevel()
    {

    }

    public void Stop()
    {
        isPlaying = false;
    }

    public void Resume()
    {
        isPlaying = false;
    }
}
