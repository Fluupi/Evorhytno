using System.Collections;
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

    [SerializeField]
    private UIVFXController _vfxController;
    
    [SerializeField]
    private UIButtonController _rhiButton;
    [SerializeField]
    private UIButtonController _noButton;
    [SerializeField]
    private UIButtonController _ceButton;
    [SerializeField]
    private UIButtonController _rosButton;

    public bool[] btnValue;

    private void Update()
    {
        if (Input.GetButtonDown("Rhi")) {
            if (btnValue[0]) {
                _vfxController.PlayVFXHit(BtnValue.Rhi);
            } else {
                _vfxController.PlayVFXMissed();
            }
        }

        if (Input.GetButtonDown("No")) {
            if (btnValue[1]) {
                _vfxController.PlayVFXHit(BtnValue.No);
            } else {
                _vfxController.PlayVFXMissed();
            }
        }

        if (Input.GetButtonDown("Ce")) {
            if (btnValue[2]) {
                _vfxController.PlayVFXHit(BtnValue.Ce);
            } else {
                _vfxController.PlayVFXMissed();
            }
        }

        if (Input.GetButtonDown("Ros")) {
            if (btnValue[3]) {
                _vfxController.PlayVFXHit(BtnValue.Ros);
            } else {
                _vfxController.PlayVFXMissed();
            }
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
        float currentTime = processedPartition.BeforeTeachTime;

        //teach
        for (int i = 0; i < processedPartition.Times.Count; i++) {
            currentTime += processedPartition.Times[i];

            if (i >= processedPartition.BtwTimes.Count)
                continue;

            currentTime += processedPartition.BtwTimes[i];
        }

        // VFX
        var animationDuration = processedPartition.BtwTeachAndListenTime+processedPartition.Times[0] / 2f;
        
        yield return new WaitForSeconds(currentTime);
        
        var nextButton = processedPartition.BtnScript[0] switch {
            BtnValue.Rhi => _rhiButton,
            BtnValue.No => _noButton,
            BtnValue.Ce => _ceButton,
            BtnValue.Ros => _rosButton,
        };
        nextButton.PlayAnimation(animationDuration);

        //between teach & listen
        
        yield return new WaitForSeconds(processedPartition.BtwTeachAndListenTime);
        
        //listen
        for (int i = 0; i < processedPartition.Times.Count; i++) {
            //allow correct input
            btnValue[(int)processedPartition.BtnScript[i]] = true;
            Debug.Log($"{processedPartition.BtnScript[i]} is up!");

            // VFX

            yield return new WaitForSeconds(processedPartition.Times[i]);

            //reset input
            btnValue[(int)processedPartition.BtnScript[i]] = false;
            Debug.Log($"{processedPartition.BtnScript[i]} end");

            if (i >= processedPartition.Times.Count - 1)
                break;

            nextButton = processedPartition.BtnScript[i+1] switch {
                BtnValue.Rhi => _rhiButton,
                BtnValue.No => _noButton,
                BtnValue.Ce => _ceButton,
                BtnValue.Ros => _rosButton,
            };
            
            nextButton.PlayAnimation(processedPartition.BtwTimes[i] + processedPartition.Times[i+1] / 2f);
            
            yield return new WaitForSeconds(processedPartition.BtwTimes[i]);
        }
    }
}