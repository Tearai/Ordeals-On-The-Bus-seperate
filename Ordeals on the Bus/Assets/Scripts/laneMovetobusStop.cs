using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laneMovetobusStop : MonoBehaviour
{
    public Transform target; // The target GameObject to move towards
    public float moveSpeed = 5.0f; // The speed at which the GameObject moves

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Move the GameObject towards the target
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
