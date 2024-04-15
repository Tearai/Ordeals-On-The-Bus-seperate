using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatephone : MonoBehaviour
{
    public GameObject screenON;
    public GameObject screenOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hand"))
        {
            screenOff.SetActive(false);
            screenON.SetActive(true);
        }
    }
}
