using System;
using UnityEngine;
using UnityEngine.Events;

public class BiomeData : MonoBehaviour
{
    [SerializeField] private BiOption[] options;

    public UnityEvent<BiOption> OnSwitch;

    public void SwitchOption()
    {
        int i = 0;

        while (options[i].Biome != GameManager.Instance.CurrentBiome)
            i++;

        GameManager.Instance.CurrentOption = options[i];
    }
}

[Serializable]
public struct BiOption
{
    public Biome Biome;

    public GameObject Scene;

    public AudioClip AmbiantAudioClip;

    public Material RhinoMat;
    public Vector3 RhinoScale;
}