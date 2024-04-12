using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dockcheck3 : MonoBehaviour
{
    public bool dockingmode;
    worldmove4 busmove4;

    // Start is called before the first frame update
    void Start()
    {
        busmove4 = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove4>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dockingmode == true)
        {

            busmove4.buspark();

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
