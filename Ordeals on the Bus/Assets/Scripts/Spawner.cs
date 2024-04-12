using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToDeactivate;
    public MonoBehaviour scriptToDeactivate;
    public MonoBehaviour scriptToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectToDeactivate != null && scriptToDeactivate != null)
            {
                // Deactivate the script on the specified GameObject
                scriptToDeactivate.enabled = false;
            }

            if (scriptToActivate != null)
            {
                // Activate the specified script
                scriptToActivate.enabled = true;
            }
        }
    }
}