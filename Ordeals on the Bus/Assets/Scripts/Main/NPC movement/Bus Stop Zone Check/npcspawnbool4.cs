using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcspawnbool4 : MonoBehaviour
{
    public bool businzone4;

    // Start is called before the first frame update
    void Start()
    {
        businzone4 = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            businzone4 = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            businzone4 = false;
        }
    }

}
