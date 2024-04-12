using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcspawnbool3 : MonoBehaviour
{
    public bool businzone3;

    // Start is called before the first frame update
    void Start()
    {
        businzone3 = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            businzone3 = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            businzone3 = false;
        }
    }

}
