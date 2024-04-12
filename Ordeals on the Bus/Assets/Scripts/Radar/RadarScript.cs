using UnityEngine;

public class RadarScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float movementRange = 34.03246f; // Maximum distance to move left or right from original position
    public float positionOffset = 1f; // Offset from center for left and right positions
    public Vector3 originalPosition; // Original position of the object

    // Reference to the worldmove script
    public worldmove worldMoveScript;

    void Start()
    {
        // Store the original position when the script starts
        originalPosition = transform.position;
    }

    void Update()
    {
        // Use the current lane index value to determine the movement direction
        float inputValue = worldMoveScript.currentLaneIndex;

        // Calculate target position based on input value
        float targetX = transform.position.x;

        if (inputValue < 1f) // Move left
        {
            targetX = Mathf.Clamp(targetX - positionOffset, originalPosition.x - movementRange, targetX);
        }
        else if (inputValue > 1f) // Move right
        {
            targetX = Mathf.Clamp(targetX + positionOffset, targetX, originalPosition.x + movementRange);
        }
        else if (inputValue == 1f) // Return to original position
        {
            targetX = originalPosition.x;
        }

        // Smoothly move the object towards the target position
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
}