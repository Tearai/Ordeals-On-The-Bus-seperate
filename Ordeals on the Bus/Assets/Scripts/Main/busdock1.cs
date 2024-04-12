using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class busdock1 : MonoBehaviour
{
    public float targetXPosition = 114.32f;
    public float moveSpeed = 5f;

    void Update()
    {
        // Move towards the target position
        float step = moveSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetXPosition, step), transform.position.y, transform.position.z);

        // You can add additional conditions to stop the movement if needed
        // For example, you can check if the current x position is close enough to the target.
        if (Mathf.Approximately(transform.position.x, targetXPosition))
        {
            // Movement completed, perform any additional actions here
        }
    }
}