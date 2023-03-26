using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public UnityEvent<ProcessedPartition> OnPlay;

    [Header("Audio Source Refs")]
    [SerializeField] private AudioSource ambianceAudioSource;
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private List<AudioSource> btnAudioSourcesTeach;
    [SerializeField] private List<AudioSource> btnAudioSourcesListen;

    [Header("Rhinoceros Sounds")]
    [SerializeField] private AudioClip[] rhinocerosAudioClips;

    public void PlayScheduled(ProcessedPartition processedPartition)
    {
        Debug.Log("Start PlayScheduled...");
        GameManager gm = GameManager.Instance;
        gm.Data.SwitchOption();

        PrepareSounds(processedPartition.BtnScript);

        ambianceAudioSource.Play();
        OnPlay.Invoke(processedPartition);

        //before teach
        double currentTime = AudioSettings.dspTime+processedPartition.BeforeTeachTime;

        //teach
        for (int i = 0; i < processedPartition.Times.Count; i++) {
            currentTime += processedPartition.Times[i] ;
            btnAudioSourcesTeach[i].PlayScheduled(currentTime);

            Debug.Log(
                $"Scheduled {btnAudioSourcesTeach[i].name} to play {btnAudioSourcesTeach[i].clip.name} at {currentTime}");

            if (i >= processedPartition.BtwTimes.Count)
                continue;

            currentTime += processedPartition.BtwTimes[i];
        }

        //between teach & listen
        currentTime += processedPartition.BtwTeachAndListenTime;

        //listen
        for (int i = 0; i < processedPartition.Times.Count; i++) {
            currentTime += processedPartition.Times[i];
            btnAudioSourcesListen[i].PlayScheduled(currentTime);
        
            Debug.Log(
                $"Scheduled {btnAudioSourcesListen[i].name} to play {btnAudioSourcesListen[i].clip.name} at {currentTime}");
        
            if (i >= processedPartition.BtwTimes.Count)
                continue;
        
            currentTime += processedPartition.BtwTimes[i];
        }

        Debug.Log("End PlayScheduled...");
    }

    private void PrepareSounds(List<BtnValue> processedPartitionBtnScript)
    {
        for (int i = 0; i < processedPartitionBtnScript.Count; i++) {
            var clip = rhinocerosAudioClips[(int)processedPartitionBtnScript[i]];
            btnAudioSourcesTeach[i].clip = clip;
            btnAudioSourcesListen[i].clip = clip;
        }
    }

    public void Stop()
    {
        // foreach (var audioSource in btnAudioSources)
        //     audioSource.Stop();
    }

    public void UpdateAmbiant(AudioClip dataAmbiantAudioClip)
    {
        ambianceAudioSource.clip = dataAmbiantAudioClip;
    }
}