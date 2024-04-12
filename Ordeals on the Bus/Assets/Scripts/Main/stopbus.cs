using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopbus : MonoBehaviour
{
    public bool busstoping;
    worldmove busstop;
    public dockcheck dock1;
    Collider coll;
    public getonbus npcbus;

    // Start is called before the first frame update
    void Start()
    {
        busstop = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove>();
        
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
            busstoping = true;
            busstop.speed = 0;
            coll.enabled = false;
            npcbus.canSpawn = true;
            dock1.dockingmode = false;

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

