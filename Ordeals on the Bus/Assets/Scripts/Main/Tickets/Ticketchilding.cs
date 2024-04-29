using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticketchilding : MonoBehaviour
{
    public GameObject Ticket;
    public bool ticketisoff;

    private Rigidbody ticketRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        ticketRigidbody = Ticket.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ticketisoff == true)
        {
            Ticket.transform.parent = null;
            ticketRigidbody.isKinematic = true;
        }

        if (ticketisoff == false)
        {
            ticketRigidbody.isKinematic = true;
        }
    }

    public void ticketoff()
    {
        ticketisoff = true;
    }
}
