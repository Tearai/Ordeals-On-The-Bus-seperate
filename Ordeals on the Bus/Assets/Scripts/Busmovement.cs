using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Busmovement : MonoBehaviour
{
    public float speed = 5.0f; // Adjust the speed as needed

    public float moveSpeed = 1.0f;

    void Update()
    {
        // Get the current position of the GameObject
        Vector3 currentPosition = transform.position;

        // Calculate the new position by moving forward in the GameObject's current direction
        Vector3 newPosition = currentPosition + transform.forward * speed * Time.deltaTime;

        // Update the GameObject's position to the new position
        transform.position = newPosition;       
    }
}
