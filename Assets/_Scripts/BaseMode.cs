using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMode : GameMode
{
    [SerializeField] private Level[] levels;

    public override void LaunchNextLevel()
    {
        if (currentLevel >= levels.Length)
        {
            GameManager.Instance.Win();
            return;
        }

        currentLevel++;
        Debug.Log($"Launch level {currentLevel}");
        bool[] level = { levels[currentLevel].Rhi, levels[currentLevel].No, levels[currentLevel].Ce, levels[currentLevel].Ros };

        partition.UpdateAvailableButtons(level);
        partition.GenerateRandomScript();
        director.Play();
        isPlaying = true;
    }
}

[Serializable]
public struct Level
{
    public bool Rhi;
    public bool No;
    public bool Ce;
    public bool Ros;
}