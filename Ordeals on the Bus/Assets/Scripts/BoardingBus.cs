using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardingBus : MonoBehaviour
{
    public string[] targetObjectNames1; // First array of target GameObject names
    public string[] targetObjectNames2; // Second array of target GameObject names
    public float speed = 5f; // The speed at which the object moves
    private int currentTargetIndex = 0; // Index to keep track of the current target
    private string[] currentTargetArray; // Current array of target names
    private bool hasPickedRandomTarget = false; // Flag to track whether a random target has been picked

    void Start()
    {
        // Start with the first array of target names
        currentTargetArray = targetObjectNames1;
    }

    void Update()
    {
        // Check if there are targets in the current array
        if (currentTargetArray.Length > 0)
        {
            // Find the current target GameObject by name
            GameObject currentTarget = GameObject.Find(currentTargetArray[currentTargetIndex]);

            // Check if the current target object is found
            if (currentTarget != null)
            {
                // Calculate the direction from the current position to the target position
                Vector3 direction = currentTarget.transform.position - transform.position;

                // Normalize the direction to get a unit vector
                direction.Normalize();

                // Move towards the current target using the MoveTowards function
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);

                // Check if the object has reached the current target
                if (Vector3.Distance(transform.position, currentTarget.transform.position) < 0.1f)
                {
                    // If using the first array, move to the next target
                    if (currentTargetArray == targetObjectNames1)
                    {
                        currentTargetIndex = (currentTargetIndex + 1) % currentTargetArray.Length;

                        // Check if we have reached the end of the first array
                        if (currentTargetIndex == 0)
                        {
                            // Switch to the second array of target names
                            currentTargetArray = targetObjectNames2;

                            // Check if a random target has not been picked yet
                            if (!hasPickedRandomTarget)
                            {
                                // Pick a random target from the second array
                                currentTargetIndex = Random.Range(0, targetObjectNames2.Length);

                                // Set the flag to true to indicate that a random target has been picked
                                hasPickedRandomTarget = true;

                                // Set the moving object as a child of the picked target
                                transform.SetParent(currentTarget.transform);
                            }
                        }
                    }
                    else
                    {
                        // If using the second array, stop moving
                        speed = 0f;
                    }
                }
            }
            else
            {
                Debug.LogError("Target object not found with name: " + currentTargetArray[currentTargetIndex]);
            }
        }
        else
        {
            Debug.LogWarning("No target objects specified.");
        }
    }
}



