using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMode : GameMode
{
    [SerializeField] private Biome[] biomeScript;
    [SerializeField] private Level[] levels;

    public override void ChooseBiome()
    {
        GameManager.Instance.UpdateData(biomeScript[currentLevel]);
    }

    public override void LaunchNextLevel()
    {
        if (++currentLevel >= levels.Length)
        {
            GameManager.Instance.Win();
            return;
        }

        progress = 0;
        Debug.Log($"Launching level {currentLevel}...");
        ChooseBiome();
        bool[] level = { levels[currentLevel].Rhi, levels[currentLevel].No, levels[currentLevel].Ce, levels[currentLevel].Ros };

        partition.UpdateAvailableButtons(level);
        PlayStep();
        isPlaying = true;
        Debug.Log($"Level {currentLevel} launched");
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