using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopbus4 : MonoBehaviour
{
    public bool busstoping;
    worldmove4 busstop;
    Collider coll;
    public busStop4 npcbus;
    public dockcheck3 dock1;

    // Start is called before the first frame update
    void Start()
    {
        busstop = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove4>();

        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dock1.dockingmode = false;
            busstoping = true;
            busstop.speed = 0;
            coll.enabled = false;
            npcbus.canSpawn4 = true;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            busstoping = false;
        }
    }
}