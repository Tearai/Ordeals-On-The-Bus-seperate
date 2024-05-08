using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCObjects : MonoBehaviour
{
    public NPC6 npc;
    public NPC4 npc4;
    private bool canwork;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canwork == true)
        {
            StartCoroutine(Timer());
            canwork = false;
        }
    }


    public void gotmustache()
    {
        canwork = true;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);
        npc.run = true;
        npc.movementSpeed = 5f;
        npc4.canDriveOff = true;
        yield return new WaitForSeconds(10f);
        npc.gotmade = true;
    }
}
