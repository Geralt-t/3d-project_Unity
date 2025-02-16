using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLocomotion : MonoBehaviour
{
    Vector3 moveDirection;
    InputManager inputManager;
    Transform cameraObject;
    Rigidbody playerRigidbody;
    public float movementSpeed = 7;
    public float rotationSpeed = 15;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>(); 
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput ;
        moveDirection = cameraObject.right * inputManager.horizontalInput +moveDirection;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = movementSpeed*moveDirection;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }
    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection=cameraObject.forward*inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;
        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation =Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation =Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);
    
        transform.rotation = playerRotation;
    }
}
