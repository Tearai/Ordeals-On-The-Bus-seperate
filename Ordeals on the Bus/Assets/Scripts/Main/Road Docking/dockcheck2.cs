using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dockcheck2 : MonoBehaviour
{
    public bool dockingmode;
    worldmove3 busmove3;

    // Start is called before the first frame update
    void Start()
    {
        busmove3 = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dockingmode == true)
        {

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

