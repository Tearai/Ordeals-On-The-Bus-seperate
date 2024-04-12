using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Wheelback : MonoBehaviour
{
    public XRKnob wheel;
    public bool CanMoveBack;
    public float transitionspeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMoveBack == true)
        {
            wheel.value = Mathf.Lerp(wheel.value, 0.5f, transitionspeed * Time.deltaTime);
        }
    }

    public void handson()
    {
        CanMoveBack = false;
    }

    public void handsoff()
    {
        CanMoveBack = true;
    }
}
