using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcspawnbool2 : MonoBehaviour
{
    public bool businzone2;

    // Start is called before the first frame update
    void Start()
    {
        businzone2 = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            businzone2 = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            businzone2 = false;
        }
    }

}

