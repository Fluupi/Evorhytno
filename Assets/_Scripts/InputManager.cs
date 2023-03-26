using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Singleton

    private static InputManager instance;
    public static InputManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }

    #endregion

    public bool[] btnValue;

    private void Update()
    {
        if (Input.GetButtonDown("Rhi")) {
            Debug.Log(btnValue[0] ? "Rhi Valide" : "Rhi Non Valide", this);
        }

        if (Input.GetButtonDown("No")) {
            Debug.Log(btnValue[1] ? "No Valide" : "No Non Valide", this);
        }

        if (Input.GetButtonDown("Ce")) {
            Debug.Log(btnValue[2] ? "Ce Valide" : "Ce Non Valide", this);
        }

        if (Input.GetButtonDown("Ros")) {
            Debug.Log(btnValue[3] ? "Ros Valide" : "Ros Non Valide", this);
        }

        if (Input.GetButtonDown("Pause")) {
            //GameManager.Instance.PauseToggle();
        }
    }

    public void LaunchProcessListening(ProcessedPartition processedPartition)
    {
        StartCoroutine(ProcessListening(processedPartition));
    }

    public IEnumerator ProcessListening(ProcessedPartition processedPartition)
    {
        //before teach
        float currentTime = (float)AudioManager.BaseTime+processedPartition.BeforeTeachTime;

        //teach
        for (int i = 0; i < processedPartition.Times.Count; i++) {
            currentTime += processedPartition.Times[i];

            if (i >= processedPartition.BtwTimes.Count)
                continue;

            currentTime += processedPartition.BtwTimes[i];
        }

        //between teach & listen
        currentTime += processedPartition.BtwTeachAndListenTime;

        yield return new WaitForSeconds(currentTime);
        currentTime = 0f;

        //listen
        for (int i = 0; i < processedPartition.Times.Count; i++) {
            //allow correct input
            btnValue[(int)processedPartition.BtnScript[i]] = true;

            currentTime += processedPartition.Times[i];
            yield return new WaitForSeconds(currentTime);
            currentTime = 0f;

            //reset input
            btnValue[(int)processedPartition.BtnScript[i]] = false;

            if (i >= processedPartition.BtwTimes.Count)
                continue;

            currentTime += processedPartition.BtwTimes[i];
            yield return new WaitForSeconds(currentTime);
            currentTime = 0f;
        }
    }
}