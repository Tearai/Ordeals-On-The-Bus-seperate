using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaVFX : MonoBehaviour
{
    public GameObject objectToActivate;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            // Activate the object
            objectToActivate.SetActive(true);
        }
    }
}