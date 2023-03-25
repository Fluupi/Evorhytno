using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityWar : GameMode
{
    public override void LaunchNextLevel()
    {
        partition.GenerateRandomScript();
        director.Play();
    }
}
