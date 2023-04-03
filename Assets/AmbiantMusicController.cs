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
            Destroy(this);

        DontDestroyOnLoad(gameObject);
        instance = this;
        _source = GetComponent<AudioSource>();
    }
    
    #endregion

    private AudioSource _source;


    public void PlayAmbiant(AudioClip clip)
    {
        if (_source == null)
            _source = GetComponent<AudioSource>();

        _source.clip = clip;
        _source.Play();
    }
}
