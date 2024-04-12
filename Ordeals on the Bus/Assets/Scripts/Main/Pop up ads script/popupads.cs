using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupads : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hand"))
        {
            Destroy(gameObject);
        }
    }

}
