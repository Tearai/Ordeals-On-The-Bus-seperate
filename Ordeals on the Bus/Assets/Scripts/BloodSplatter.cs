using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the object
            objectToActivate.SetActive(true);
            objectToDeactivate.SetActive(false);
        }
    }
}
