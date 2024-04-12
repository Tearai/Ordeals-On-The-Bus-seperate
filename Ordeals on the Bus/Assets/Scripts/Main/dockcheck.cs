using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dockcheck : MonoBehaviour
{
    public bool dockingmode;
    worldmove busmove;
    worldmove3 busmove2;
    worldmove4 busmove3;


    // Start is called before the first frame update
    void Start()
    {
        busmove = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove>();
        busmove2 = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove3>();
        busmove3 = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove4>();



    }

    // Update is called once per frame
    void Update()
    {
        if(dockingmode == true)
        {
            busmove.buspark();
            busmove2.buspark();
            busmove3.buspark();
 
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dockingmode = true;
        }
    }
}
