using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Partition : MonoBehaviour
{
    private bool[] availableButtons;
    [SerializeField] private TimeBorders _beforeTeachTimeBorders, _timeBorders, _btwTimeBorders, _btwTeachAndListenTimeBorders;

    public void UpdateAvailableButtons(bool[] newSet)
    {
        availableButtons = newSet;
    }

    public void UpdateTimeBorders(TimeBorders beforeTeachTimeBorders, TimeBorders timeBorders, TimeBorders btwTimeBorders, TimeBorders btwTeachAndListenTimeBorders)
    {
        _beforeTeachTimeBorders       = beforeTeachTimeBorders;
        _timeBorders                  = timeBorders;
        _btwTimeBorders               = btwTimeBorders;
        _btwTeachAndListenTimeBorders = btwTeachAndListenTimeBorders;
    }

    public ProcessedPartition GenerateRandomScript(int length = 3)
    {
        ProcessedPartition processedPartition = new ProcessedPartition
        {
            BtnScript = new List<BtnValue>(),
            BtwTimes = new List<float>(),
            Times = new List<float>()
        };

        //buttons choice
        for (int i = 0; i < length; i++)
        {
            int btn = Random.Range(0, 4);

            while (!availableButtons[btn])
                btn = Random.Range(0, 4);

            processedPartition.BtnScript.Add((BtnValue)btn);
        }
        
        //before teach Time
        processedPartition.BeforeTeachTime = 
            Random.Range(_beforeTeachTimeBorders.Min, _beforeTeachTimeBorders.Max);

        //teachTimes
        for (int i = 0; i < length; i++)
            processedPartition.Times.Add(Random.Range(_timeBorders.Min, _timeBorders.Max));

        //between TeachTimes
        for (int i = 0; i < length-1; i++)
            processedPartition.BtwTimes.Add(Random.Range(_btwTimeBorders.Min, _btwTimeBorders.Max));

        //between teach and listen Time
        processedPartition.BtwTeachAndListenTime =
            Random.Range(_btwTeachAndListenTimeBorders.Min, _btwTeachAndListenTimeBorders.Max);
        
        return processedPartition;
    }
}

[Serializable]
public struct ProcessedPartition
{
    public List<BtnValue> BtnScript;
    public float BeforeTeachTime;
    public List<float> Times;
    public List<float> BtwTimes;
    public float BtwTeachAndListenTime;
}

[Serializable]
public struct TimeBorders
{
    public float Min;
    public float Max;
}

public enum BtnValue
{
    Rhi,
    No,
    Ce,
    Ros
}