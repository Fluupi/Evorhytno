using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public UnityEvent<ProcessedPartition> OnPlay;

    [Header("Audio Source Refs")]
    [SerializeField] private AudioSource ambianceAudioSource;
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private List<AudioSource> btnAudioSources;

    [Header("Rhinoceros Sounds")]
    [SerializeField] private AudioClip[] rhinocerosAudioClips;

    public void PlayScheduled(ProcessedPartition processedPartition)
    {
        Debug.Log("Start PlayScheduled...");
        GameManager gm = GameManager.Instance;
        gm.Data.SwitchOption();

        PrepareSounds(processedPartition.BtnScript);

        int audioSourceIndex = 0;

        ambianceAudioSource.Play();
        OnPlay.Invoke(processedPartition);

        //before teach
        double currentTime = processedPartition.BeforeTeachTime;

        //teach
        for (int i = 0; i < processedPartition.Times.Count; i++)
        {
            currentTime += processedPartition.Times[i];
            btnAudioSources[audioSourceIndex].Play();//Scheduled(currentTime);
            audioSourceIndex++;

            if (i >= processedPartition.BtwTimes.Count)
                continue;

            currentTime += processedPartition.BtwTimes[i];
        }

        //between teach & listen
        currentTime += processedPartition.BtwTeachAndListenTime;

        //listen
        for (int i = 0; i < processedPartition.Times.Count; i++)
        {
            currentTime += processedPartition.Times[i];
            btnAudioSources[audioSourceIndex].Play();//Scheduled(currentTime);
            audioSourceIndex++;

            if (i >= processedPartition.BtwTimes.Count)
                continue;

            currentTime += processedPartition.BtwTimes[i];
        }
        Debug.Log("End PlayScheduled...");
    }

    private void PrepareSounds(List<BtnValue> processedPartitionBtnScript)
    {
        for(int i=0; i<3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                btnAudioSources[i * 2 + j].clip = rhinocerosAudioClips[(int)processedPartitionBtnScript[i]];
            }
        }
    }

    public void Stop()
    {
        foreach (var audioSource in btnAudioSources)
            audioSource.Stop();
    }

    public void UpdateAmbiant(AudioClip dataAmbiantAudioClip)
    {
        ambianceAudioSource.clip = dataAmbiantAudioClip;
    }
}