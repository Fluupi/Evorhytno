using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public void Callback(InputAction.CallbackContext context)
    {
        Debug.Log(context.action.name);
    }
}
