using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaflinch3 : MonoBehaviour
{
    public NPC4 npc;

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