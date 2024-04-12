using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class nomovementSlider : MonoBehaviour
{

    public XRSlider slider;
    public XRKnob wheel;
    public float defaultspeed = 0.3f;
    public float transitionspeed = 1f;
    public bool canSlider;

    public Collider siliderbox;
    public Collider wheelbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSlider == true)
        {
            siliderbox.enabled = false;
            wheelbox.enabled = false;

            wheel.value = Mathf.Lerp(wheel.value, 0.5f, transitionspeed * Time.deltaTime);

            if (slider.value != defaultspeed)
            {
                slider.value = Mathf.Lerp(slider.value, defaultspeed, transitionspeed * Time.deltaTime);

            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSlider = true;
        }
            
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSlider = false;
            siliderbox.enabled = true;
            wheelbox.enabled = true;
        }
            
    }
}
