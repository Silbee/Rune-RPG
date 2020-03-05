using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunC : MonoBehaviour
{
    Vector2 moveVector;
    public int movementSpeed = 0;

    public Animator animator;

    public void Run(InputAction.CallbackContext context)
    {
        Vector2 wasdVector = context.ReadValue<Vector2>();
        moveVector = wasdVector * movementSpeed;

        animator.SetFloat("Horizontal",wasdVector.x);
        animator.SetFloat("Vertical",wasdVector.y);
        animator.SetFloat("Magnitude",wasdVector.magnitude);
    }
    public void Update()
    {
        transform.Translate(moveVector.x * Time.deltaTime, moveVector.y * Time.deltaTime, 0);
    }
}
