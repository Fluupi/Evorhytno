using System;
using UnityEngine;

[RequireComponent(typeof(Partition))]
public class ProceduralMode : GameMode
{
    [SerializeField] private Partition partition;

    [Header("ScriptedMode")]
    [SerializeField] private ProceduralModeLevel[] levels;

    private void Start()
    {
        partition = GetComponent<Partition>();
    }

    public override Biome GetBiome()
    {
        return levels[currentLevel].Biome;
    }

    public override ProcessedPartition GetPartition()
    {
        partition.UpdateAvailableButtons(new bool[] { levels[currentLevel].Rhi, levels[currentLevel].No, levels[currentLevel].Ce, levels[currentLevel].Ros });
        return partition.GenerateRandomScript();
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
public struct ProceduralModeLevel
{
    public Biome Biome;

    public bool Rhi;
    public bool No;
    public bool Ce;
    public bool Ros;
}