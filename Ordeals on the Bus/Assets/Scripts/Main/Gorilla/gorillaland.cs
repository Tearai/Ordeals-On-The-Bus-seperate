using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gorillaland : MonoBehaviour
{
    public bool touchedGround;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            touchedGround = true;
        }
    }
}
