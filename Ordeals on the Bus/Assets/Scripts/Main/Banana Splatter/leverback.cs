using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class leverback : MonoBehaviour
{
    public Transform leverTransform; // Reference to the lever's Transform component
    public float rotationSpeed = 1f; // Speed at which the lever rotates back to its original position
    public float targetAngle = -50f; // Target angle to rotate back to
    public bool canTurn = true; // Flag to indicate if the lever can turn

    private Quaternion originalRotation; // Stores the lever's original rotation
    private Quaternion targetRotation; // Stores the target rotation
    public bool hasReachedTarget = false; // Flag to indicate if the lever has reached the target angle

    public XRLever lever;


    private void Start()
    {
        // Store the original rotation of the lever
        originalRotation = leverTransform.localRotation;
        // Calculate the target rotation based on the target angle
        targetRotation = Quaternion.Euler(targetAngle, 0f, 0f);
    }

    private void Update()
    {
        if(lever.value == true)
        {
            moveBack();
        }
        
    }

    // Coroutine to reset the lever's rotation to the original position over time
    IEnumerator ResetLeverRotation(float time)
    {
        yield return new WaitForSeconds(time);

        // Reset the target rotation to the original rotation
        targetRotation = originalRotation;
        hasReachedTarget = false; // Reset the flag
    }

    // Method to trigger the lever reset coroutine
    public void StartResetRotation(float delay)
    {
        StartCoroutine(ResetLeverRotation(delay));
    }

    public void moveBack()
    {
        

        if (!hasReachedTarget && leverTransform.localRotation != targetRotation)
        {
            // Smoothly rotate the lever back to the target angle using Lerp
            leverTransform.localRotation = Quaternion.Lerp(leverTransform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Check if the lever has reached the target angle within a small threshold
            if (Quaternion.Angle(leverTransform.localRotation, targetRotation) < 1f)
            {
                hasReachedTarget = true;
                lever.value = false;

            }
        }
    }

    public void handson()
    {
        canTurn = false;
    }

    public void handsoff()
    {
        canTurn = true;
    }
}