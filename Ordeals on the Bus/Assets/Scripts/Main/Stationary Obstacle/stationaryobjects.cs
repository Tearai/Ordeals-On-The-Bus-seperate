using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stationaryobjects : MonoBehaviour
{
    public Transform stationaryObject;
    public float launchForce;
    public Collider triggerCollider;
    public bool canHit;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bus"))
        {
            if (canHit == false)
            {
                Rigidbody rb = stationaryObject.GetComponent<Rigidbody>();

                rb.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);

                triggerCollider.enabled = false;

                canHit = true;

                Vector3 newSize = stationaryObject.localScale;
                newSize.z = 2.5f;
                stationaryObject.localScale = newSize;
            }

        }
    }
}