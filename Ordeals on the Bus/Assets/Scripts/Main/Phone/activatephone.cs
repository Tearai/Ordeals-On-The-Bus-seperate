using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatephone : MonoBehaviour
{
    public GameObject screenON;
    public GameObject screenOff;
    private bool canworknow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(canworknow)
        {
            screenOff.SetActive(false);
            screenON.SetActive(true);
            canworknow = false;
        }
    }   

    public void phoneon()
    {
        canworknow = true;
    }
}
