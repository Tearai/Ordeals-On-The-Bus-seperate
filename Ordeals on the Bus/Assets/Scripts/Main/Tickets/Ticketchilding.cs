using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticketchilding : MonoBehaviour
{
    public GameObject Ticket;
    public bool ticketisoff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ticketisoff == true)
        {
            Ticket.transform.parent = null;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            ticketisoff = true;
        }
    }
}
