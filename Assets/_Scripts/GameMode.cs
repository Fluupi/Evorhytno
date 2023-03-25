using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MonoBehaviour
{
    [SerializeField] protected float gameSpeed;
    [SerializeField] protected bool isPlaying;
    public bool IsPlaying => isPlaying;

    public virtual void LaunchGame()
    {
        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying = false;
    }
}
