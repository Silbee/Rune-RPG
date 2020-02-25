using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunC : MonoBehaviour
{
    public void Run(InputAction.CallbackContext context)
    {
        Vector2 wasdVector = context.ReadValue<Vector2>();
    }

}
