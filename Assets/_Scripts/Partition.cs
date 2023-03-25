using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partition : MonoBehaviour
{
    private bool[] availableButtons;
    [SerializeField] private List<BtnValue> _script;

    public void UpdateAvailableButtons(bool[] newSet)
    {
        availableButtons = newSet;
    }

    public void GenerateRandomScript(int length = 3)
    {
        for (int i = 0; i < length; i++)
        {
            int btn = Random.Range(0, 4);

            while (!availableButtons[btn])
                btn = Random.Range(0, 4);

            _script.Add((BtnValue)btn);
        }
    }
}

public enum BtnValue
{
    Rhi,
    No,
    Ce,
    Ros
}