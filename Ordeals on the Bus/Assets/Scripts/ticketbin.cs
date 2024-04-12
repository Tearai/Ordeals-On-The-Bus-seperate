using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ticketbin : MonoBehaviour
{
    bool tickettrue;
    public npcmovement npc;
    public NPC2 npc2;
    public NPC3 npc3;
    public NPC4 npc4;
    public NPC5 npc5;

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
        if(other.gameObject.CompareTag("Ticket1"))
        {
            npc.ticket1 = true;
            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("Ticket2"))
        {
            npc2.ticket2 = true;
            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("Ticket3"))
        {
            npc3.ticket3 = true;
            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("Ticket4"))
        {
            npc4.ticket4 = true;
            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("Ticket5"))
        {
            npc5.ticket5 = true;
            Destroy(other.gameObject);

        }
    }

    public IEnumerator ItemDelay()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
