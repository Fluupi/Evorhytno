using System;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedMode : GameMode
{
    [Header("ScriptedMode")]
    [SerializeField] private float _beforeTeachTime;
    [SerializeField] private float _timeBorders;
    [SerializeField] private float _btwTimeBorders;
    [SerializeField] private float _btwTeachAndListenTimeBorders;
    [Space]
    [SerializeField] private ScriptedModeLevel[] levels;

    public override Biome GetBiome()
    {
        return levels[currentLevel].Biome; 
    }

    public override ProcessedPartition GetPartition()
    {
        return new ProcessedPartition()
        {
            BtnScript = new List<BtnValue>()
            {
                levels[currentLevel].btn1,
                levels[currentLevel].btn2,
                levels[currentLevel].btn3,
            },
            BeforeTeachTime = _beforeTeachTime,
            Times = new List<float>()
            {
                _timeBorders,
                _timeBorders,
                _timeBorders
            },
            BtwTimes = new List<float>()
            {
                _btwTimeBorders,
                _btwTimeBorders
            },
            BtwTeachAndListenTime = _btwTeachAndListenTimeBorders
        };
    }

    public override void LaunchNextLevel()
    {
        if (++currentLevel >= levels.Length)
        {
            isPlaying = false;
            GameManager.Instance.Win();
            return;
        }

        progress = 0;
        Debug.Log($"Launching level {currentLevel}...");

        StartCoroutine(GameManager.Instance.UpdateData(levels[currentLevel].Biome));
        GameManager.Instance.PlayTransition(levels[currentLevel].Biome);
        
        PlayStep();
        isPlaying = true;
        Debug.Log($"Level {currentLevel} launched");
    }
}

[Serializable]
public struct ScriptedModeLevel
{
    public Biome Biome;

    public BtnValue btn1;
    public BtnValue btn2;
    public BtnValue btn3;
}