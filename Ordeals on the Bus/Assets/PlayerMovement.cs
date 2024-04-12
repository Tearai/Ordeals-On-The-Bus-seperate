using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float moveDistance = 1f; // Distance to move left or right
    public float moveTime = 0.5f; // Time taken to move to new position

    private bool isMoving = false; // Flag to track if the player is currently moving
    private Vector3 targetPosition; // Target position to move to

    void Update()
    {
        // Move left (decrease Z) when 'A' key is pressed
        if (Input.GetKeyDown(KeyCode.A) && !isMoving)
        {
            MoveLeft();
        }
        // Move right (increase Z) when 'D' key is pressed
        else if (Input.GetKeyDown(KeyCode.D) && !isMoving)
        {
            MoveRight();
        }

        // Move towards the target position if the player is currently moving
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if we have reached close enough to the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition; // Snap to the exact target position
                isMoving = false; // Stop moving
            }
        }
    }

    void MoveLeft()
    {
        // Calculate new position
        Vector3 newPosition = transform.position + Vector3.back * moveDistance;

        // Start moving towards the new position
        StartCoroutine(MoveToPosition(newPosition));
    }

    void MoveRight()
    {
        // Calculate new position
        Vector3 newPosition = transform.position + Vector3.forward * moveDistance;

        // Start moving towards the new position
        StartCoroutine(MoveToPosition(newPosition));
    }

    IEnumerator MoveToPosition(Vector3 newPosition)
    {
        isMoving = true; // Set moving flag to true
        targetPosition = newPosition; // Set target position

        float elapsedTime = 0f; // Track time elapsed since start of movement

        // Continue moving until we reach the target position
        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure we end up exactly at the target position
        isMoving = false; // Reset moving flag
    }
}