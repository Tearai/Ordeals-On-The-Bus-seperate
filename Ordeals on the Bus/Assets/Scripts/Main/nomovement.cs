using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class nomovement : MonoBehaviour
{
    worldmove busmove;
    worldmove3 busmove3;
    worldmove4 busmove4;
   

    // Start is called before the first frame update
    void Start()
    {
        busmove = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove>();
        busmove3 = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove3>();
        busmove4 = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove4>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            busmove.isSwitchingLane = true;
            busmove.canspeed = false;

            busmove3.isSwitchingLane = true;
            busmove3.canspeed = false;

            busmove4.isSwitchingLane = true;
            busmove4.canspeed = false;


        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            busmove.isSwitchingLane = false;
            busmove.canspeed = true;

            busmove3.isSwitchingLane = false;
            busmove3.canspeed = true;

            busmove4.isSwitchingLane = false;
            busmove4.canspeed = true;
        }
    }

}
