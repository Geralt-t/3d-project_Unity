using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerController;
    AnimatorManager animatorManager;
   public Vector2 movementInput;
    private float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();  
    }
    private void OnEnable()
    {
        if(playerController == null)
        {
            playerController=new PlayerController();
            playerController.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        playerController.Enable();
    }
    private void OnDisable()
    {
        playerController.Disable();
    }
    public void HandleAllInputs()
    {
        HandleMovementInput();

    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput=movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput)+Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0,moveAmount);
    }
}
