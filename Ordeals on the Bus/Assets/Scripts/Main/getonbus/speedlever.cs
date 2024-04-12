using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedlever : MonoBehaviour
{
    worldmove lever;
    // Start is called before the first frame update
    void Start()
    {
        lever = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(lever.canspeed == true)
        {
            if (other.gameObject.CompareTag("Super Fast"))
            {
                lever.speed = 2f;
            }

            if (other.gameObject.CompareTag("Fast"))
            {
                lever.speed = 1.5f;
            }

            if (other.gameObject.CompareTag("Normal"))
            {
                lever.speed = 1f;
            }

            if (other.gameObject.CompareTag("Slow"))
            {
                lever.speed = 0.5f;
            }
            if (other.gameObject.CompareTag("Super Slow"))
            {
                lever.speed = 0f;
            }
        }
    }
}
