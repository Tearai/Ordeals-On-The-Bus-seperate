using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Busdock : MonoBehaviour
{
    public GameObject busstop;
    public GameObject docking;

    public dockcheck dockk;

    //check collider for dock
    public bool dockingmode = false;

    //speed 
    public float dockingVelocity = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dockk.dockingmode == true)
        {
            // Calculate direction to docking
            Vector3 directionToDocking = docking.transform.position - busstop.transform.position;

            // Normalize the direction and multiply by constant velocity
            Vector3 velocity = directionToDocking.normalized * dockingVelocity;

            // Move the busstop
            busstop.transform.Translate(velocity * Time.deltaTime);
        }
    }
}
