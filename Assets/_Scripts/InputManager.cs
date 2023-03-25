using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public bool Rhi;
    public bool No;
    public bool Ce;
    public bool Ros;

    public void OnRhi()
    {
        Debug.Log(Rhi ? "Rhi Valide" : "Rhi Non Valide", this);
    }

    public void OnNo()
    {
        Debug.Log(No ? "No Valide" : "No Non Valide", this);
    }

    public void OnCe()
    {
        Debug.Log(Ce ? "Ce Valide" : "Ce Non Valide", this);
    }

    public void OnRos()
    {
        Debug.Log(Rhi ? "Ce Valide" : "Ce Non Valide", this);
    }

    public void Callback(InputAction.CallbackContext context)
    {
        Debug.Log(context.action.name, this);

        switch (context.action.name)
        {
            case "Rhi":
                Debug.Log(Rhi ? "Rhi Valide" : "Rhi Non Valide", this);
                break;
            case "No":
                if (No)
                    Debug.Log("No Valide", this);
                else
                    Debug.Log("No Non Valide", this);
                break;
            case "Ce":
                if (Ce)
                    Debug.Log("Ce Valide", this);
                else
                    Debug.Log("Ce Non Valide", this);
                break;
            case "Ros":
                if (Ros)
                    Debug.Log("Ros Valide", this);
                else
                    Debug.Log("Ros Non Valide", this);
                break;
            default:
                GameManager.Instance.PauseToggle();
                break;
        }
    }
}
