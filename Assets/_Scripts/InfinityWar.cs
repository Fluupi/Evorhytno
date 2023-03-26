using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityWar : GameMode
{
    public override void ChooseBiome()
    {
        Biome b = (Biome)Random.Range(0, 4);

        while (b == GameManager.Instance.CurrentBiome)
            b = (Biome)Random.Range(0, 4);

        GameManager.Instance.CurrentBiome = b;
    }

    public override void LaunchNextLevel()
    {
        currentLevel++;
        ChooseBiome();
        audioManager.PlayScheduled(partition.GenerateRandomScript());
    }
}
