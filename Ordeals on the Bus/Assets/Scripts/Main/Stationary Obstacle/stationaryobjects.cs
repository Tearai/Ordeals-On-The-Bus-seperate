using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stationaryobjects : MonoBehaviour
{
    public Transform stationaryObject;
    public float launchForce;
    public Collider triggerCollider;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bus"))
        {
            Rigidbody rb = stationaryObject.GetComponent<Rigidbody>();

            rb.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);

            triggerCollider.enabled = false;
        }
    }
}