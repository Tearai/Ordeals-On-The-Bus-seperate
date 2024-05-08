using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radarchange : MonoBehaviour
{
    public RadarScript lanechange;

    public bool mafia;
    public bool goRight;
    public bool mainRoad;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (mafia == true)
            {
                lanechange.lane1 = false;
                lanechange.lane2 = true;
            }

            if (goRight == true)
            {
                lanechange.lane1 = false;
                lanechange.lane3 = true;
            }

            if(mainRoad == true)
            {
                lanechange.lane3 = false;
                lanechange.lane4 = true;
            }
        }
    }
}
