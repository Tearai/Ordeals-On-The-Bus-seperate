using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaterLeaker : MonoBehaviour
{
    // Adjust these variables to customize the behavior of the hole
    public float timeToLive = 5f; // Time before the hole disappears
    public float holeSize = 1f; // Size of the hole
    public GameObject waterPrefab; // Prefab for the water effect

    private void Start()
    {
        // Set the size of the hole
        transform.localScale = new Vector3(holeSize, holeSize, 0.1f);

        // Schedule the hole to be destroyed after a certain time
        Destroy(gameObject, timeToLive);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // If it's the player, create the water effect
            Instantiate(waterPrefab, transform.position, Quaternion.identity);

            // Destroy the hole
            Destroy(gameObject);
        }
    }
}