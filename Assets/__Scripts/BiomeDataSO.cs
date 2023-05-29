using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BiomeData")]
public class BiomeDataSO : ScriptableObject
{
    public Biome Type;

    public AudioClip AmbiantAudioClip;

    public Vector3 RhinoScale;
    public Material RhinoMat;
}
