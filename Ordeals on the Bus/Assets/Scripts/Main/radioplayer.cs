using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class radioplayer : MonoBehaviour
{
    public XRKnob turnswitch;

    public float wheelangle;

    public GameObject radio1;
    public GameObject radio2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        wheelangle = turnswitch.value;

        if (wheelangle == 1)
        {
            radio1.SetActive(true);
            radio2.SetActive(false);
        }

        if (wheelangle == 0)
        {
            radio2.SetActive(true);
            radio1.SetActive(false);
        }

        if (wheelangle > 0.4f && wheelangle < 0.6f) // Added condition
        {
            radio1.SetActive(false);
            radio2.SetActive(false);
        }
    }
}


