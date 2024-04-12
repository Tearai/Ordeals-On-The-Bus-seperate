using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class getonbus : MonoBehaviour
{
    public List<GameObject> npc = new List<GameObject>(); // Keep track of spawned NPCs
    public int currentNPCIndex = 0; // Keep track of the current NPC index

    public GameObject doorbutton;


    public bool canSpawn;
    public GameObject busdoor;
    public Animator busdoorAnim;
    public string animationName;

    public npcmovement npc1;
    //bool
    public npcspawnbool inZone;
    


    void Start()
    {
        busdoorAnim = busdoor.GetComponent<Animator>();
        
    }

    void Update()
    {
        ActivateNPC();
    }

    public void GetOn()
    {
        if (canSpawn == true && inZone.businzone1 == true)
        {
            busdoorAnim.Play(animationName);
            StartCoroutine(DoorOpening());
        }
    }


   

    IEnumerator DoorOpening()
    {
        yield return new WaitForSeconds(2.5f);
        currentNPCIndex = 0;
        npc[currentNPCIndex].GetComponent<npcmovement>().enabled = true;
        npc[currentNPCIndex].GetComponent<NavMeshAgent>().enabled = true;

    }

    void ActivateNPC()
    {
        if (npc1.ticket1 == true)
        {
            currentNPCIndex = 1;
            npc[currentNPCIndex].GetComponent<NPC7>().enabled = true;
            npc[currentNPCIndex].GetComponent<NavMeshAgent>().enabled = true;
        }

    }
}
