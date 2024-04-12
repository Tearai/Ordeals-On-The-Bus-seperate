using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
    public Transform firePoint; // The point where the cannonball will be instantiated
    public GameObject cannonballPrefab; // Prefab of the cannonball
    public float launchForce = 10f; // Force applied to the cannonball

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FireCannon();
        }

    }

    void FireCannon()
    {
        // Instantiate a cannonball at the firePoint position and rotation
        GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component of the cannonball    
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();

        // Check if the Rigidbody component exists
        if (rb != null)
        {
            // Apply force to the cannonball in the forward direction
            rb.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Cannonball prefab is missing Rigidbody component!");
        }
    }
}
