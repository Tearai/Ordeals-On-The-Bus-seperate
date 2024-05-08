using UnityEngine;

public class RadarScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float movementRange = 34.03246f; // Maximum distance to move left or right from original position
    public float positionOffset = 1f; // Offset from center for left and right positions
    public Vector3 originalPosition; // Original position of the object

    // Reference to the worldmove script
    public worldmove worldMoveScript;
    public worldmove2 worldMoveScript2;
    public worldmove3 worldMoveScript3;
    public worldmove4 worldMoveScript4;
    public float inputValue;

    public bool lane1;
    public bool lane2;
    public bool lane3;
    public bool lane4;

    void Start()
    {
        // Store the original position when the script starts
        originalPosition = transform.position;
        lane1 = true;
    }

    void Update()
    {
        if (lane1 == true)
        {
            inputValue = worldMoveScript.currentLaneIndex;
        }

        if (lane2 == true)
        {
            inputValue = worldMoveScript2.currentLaneIndex;
        }

        if (lane3 == true)
        {
            inputValue = worldMoveScript3.currentLaneIndex;
        }

        if(lane4 == true)
        {
            inputValue = worldMoveScript4.currentLaneIndex;
        }

        // Use the current lane index value to determine the movement direction

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