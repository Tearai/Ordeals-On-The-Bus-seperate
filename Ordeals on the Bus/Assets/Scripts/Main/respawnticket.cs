using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnticket : MonoBehaviour
{
    public Transform respawnpoint;
    public GameObject respawnSFX;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ticket1") || other.gameObject.CompareTag("Ticket2") || other.gameObject.CompareTag("Ticket3") ||
            other.gameObject.CompareTag("Ticket4") || other.gameObject.CompareTag("Ticket5") || other.gameObject.CompareTag("gun"))
        {
            // Resetting position and velocity
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }

            other.gameObject.transform.position = respawnpoint.position;
            StartCoroutine(ticketsfx());
        }
    }

    IEnumerator ticketsfx()
    {
        respawnSFX.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        respawnSFX.SetActive(false);
    }
}
