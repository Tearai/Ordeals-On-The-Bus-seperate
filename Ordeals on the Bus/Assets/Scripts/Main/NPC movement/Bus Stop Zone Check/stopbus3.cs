using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopbus3 : MonoBehaviour
{
    public bool busstoping;
    public dockcheck dock1;
    worldmove busstop;
    Collider coll;
    public busStop2 npcbus;

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
            npcbus.canSpawn2 = true;
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