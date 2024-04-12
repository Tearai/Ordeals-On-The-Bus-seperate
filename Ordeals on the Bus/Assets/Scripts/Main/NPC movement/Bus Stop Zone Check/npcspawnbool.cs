using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcspawnbool : MonoBehaviour
{
    public bool businzone1;

    // Start is called before the first frame update
    void Start()
    {
        businzone1 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            businzone1 = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            businzone1 = false;
        }
    }

}
