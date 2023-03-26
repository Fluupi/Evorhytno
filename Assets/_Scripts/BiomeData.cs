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
        
        OnSwitch.Invoke(options[i]);
    }
}

[Serializable]
public struct BiOption
{
    public Biome Biome;

    public GameObject Scene;

    public AudioClip AmbiantAudioClip;

    public Sprite RhinoSprite;
    public Vector3 RhinoScale;
}