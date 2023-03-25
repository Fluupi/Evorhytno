using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMode : GameMode
{
    [SerializeField] private Partition partition;
    [SerializeField] private Level[] levels;

    public override void LaunchGame()
    {
        base.LaunchGame();

        bool[] level = new[] { levels[0].Rhi, levels[0].No, levels[0].Ce, levels[0].Ros };

        partition.UpdateAvailableButtons(level);
    }

    private void Update()
    {
        while (isPlaying)
        {
            
        }
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