using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class TrafficSystem : MonoBehaviour
{
    public XRSlider slider;
    public XRKnob wheel;

    public float transitionspeed;

    public float defaultspeed = 0.3f;

    public bool crashed;

    public splatterclean splat;

    public void Update()
    {
        if(crashed == true)
        {
            wheel.value = Mathf.Lerp(wheel.value, 0.5f, transitionspeed * Time.deltaTime);

            if (slider.value != defaultspeed)
            {
                slider.value = Mathf.Lerp(slider.value, defaultspeed, transitionspeed * Time.deltaTime);

            }
        }
        
    }



    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Traffic"))
        {
            crashed = true;
            
        }

        if(other.gameObject.CompareTag("Splat"))
        {
            splat.canShow = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Traffic"))
        {
            crashed = false;
            
        }

        if (other.gameObject.CompareTag("Splat"))
        {
            splat.canShow = false;
            splat.leverTrue = false;
            splat.leverSecond = false;
            splat.leverThird = false;
            splat.leverFourth = false;
            splat.leverFifth = false;
            splat.leverSixth = false;
            splat.leverStart = false;
        }
    }
}
