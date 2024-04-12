using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class busleaving : MonoBehaviour
{
    NPC3 busleave;
    npcmovement busleave1;
    NPC4 busleave2;
    NPC5 busleave3;
    NPC2 busleave4;
    public bool test;

    // Start is called before the first frame update
    void Start()
    {
        busleave = GameObject.FindGameObjectWithTag("Second NPC").GetComponent<NPC3>();
        busleave1 = GameObject.FindGameObjectWithTag("First NPC").GetComponent<npcmovement>();
        busleave2 = GameObject.FindGameObjectWithTag("Third NPC").GetComponent<NPC4>();
        busleave3 = GameObject.FindGameObjectWithTag("Fourth NPC").GetComponent<NPC5>();
        busleave4 = GameObject.FindGameObjectWithTag("Fifth NPC").GetComponent<NPC2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(test == true)
        {
            busleave.ticket3 = false;
            busleave1.ticket1 = false;
            busleave2.canDriveOff = false;
            busleave2.ticket4 = false;
            busleave3.ticket5 = false;
            busleave4.ticket2 = false;
            busleave1.canDriveOff = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            test = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            test = false;
        }
    }
}
