using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class busStop2 : MonoBehaviour
{
    public List<GameObject> npc = new List<GameObject>(); // Keep track of spawned NPCs
    public int currentNPCIndex = 0; // Keep track of the current NPC index

    public GameObject doorbutton;


    public NPC2 npc1;
    public bool canSpawn2;
    public GameObject busdoor;
    public Animator busdoorAnim;
    public string animationName;

    //bool
    public npcspawnbool2 inZone;

    public GameObject doorSFX;
    public GameObject closedoorSFX;


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
        if (canSpawn2 == true && inZone.businzone2 == true)
        {
            busdoorAnim.Play(animationName);
            StartCoroutine(DoorOpening());
            doorSFX.SetActive(true);
            closedoorSFX.SetActive(false);
        }
    }

    void ActivateNPC()
    {
        if (npc1.ticket2 == true)
        {
            StartCoroutine(waitfornextpassenger());
        }

    }


    IEnumerator DoorOpening()
    {
        yield return new WaitForSeconds(2.5f);
        currentNPCIndex = 0;
        npc[currentNPCIndex].GetComponent<NPC2>().enabled = true;
        npc[currentNPCIndex].GetComponent<NavMeshAgent>().enabled = true;

    }

    IEnumerator waitfornextpassenger()
    {
        yield return new WaitForSeconds(10f);
        currentNPCIndex = 1;
        npc[currentNPCIndex].GetComponent<NPC3>().enabled = true;
        npc[currentNPCIndex].GetComponent<NavMeshAgent>().enabled = true;
    }
}
