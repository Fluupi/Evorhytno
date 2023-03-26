using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiantMusicController : MonoBehaviour
{
    #region Singleton

    private static AmbiantMusicController instance;
    public static AmbiantMusicController Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        instance = this;
        _source = GetComponent<AudioSource>();
    }
    
    #endregion

    private AudioSource _source;


    public void PlayAmbiant(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }
}
