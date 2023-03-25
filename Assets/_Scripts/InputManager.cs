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
    
    public bool Rhi;
    public bool No;
    public bool Ce;
    public bool Ros;

    private void Update()
    {
        if (Input.GetButtonDown("Rhi"))
        {
            Debug.Log(Rhi ? "Rhi Valide" : "Rhi Non Valide", this);
        }

        if (Input.GetButtonDown("No"))
        {
            Debug.Log(No ? "No Valide" : "No Non Valide", this);
        }

        if (Input.GetButtonDown("Ce"))
        {
            Debug.Log(Ce ? "Ce Valide" : "Ce Non Valide", this);
        }

        if (Input.GetButtonDown("Ros"))
        {
            Debug.Log(Ros ? "Ros Valide" : "Ros Non Valide", this);
        }

        if (Input.GetButtonDown("Pause"))
        {
            //GameManager.Instance.PauseToggle();
        }
    }
}
