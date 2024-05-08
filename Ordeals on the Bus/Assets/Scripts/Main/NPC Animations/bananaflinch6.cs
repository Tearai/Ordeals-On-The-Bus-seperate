using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaflinch6 : MonoBehaviour
{
    public NPC5 npc;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            npc.flinch();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            npc.noflinch();
        }
    }
}

