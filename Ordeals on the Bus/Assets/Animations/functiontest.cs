using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class functiontest : MonoBehaviour
{
    public Transform target; // The target object to look at
    public float rotationSpeed = 5f; // Speed of rotation

    void Update()
    {
        if (target != null)
        {
            // Get the direction to the target
            Vector3 direction = target.position - transform.position;
            direction.y = 0f; // Ignore the y-component

            // Smoothly rotate towards the target on the y-axis only
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}