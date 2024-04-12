using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class busvrbuttons : MonoBehaviour
{
    public Transform buttonDown;
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            if (!isPressed)
            {
                button.transform.localPosition = buttonDown.localPosition;
                presser = other.gameObject;
                onPress.Invoke();
                sound.Play();
                isPressed = true;
            }
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            if (other.gameObject == presser)
            {
                button.transform.localPosition = new Vector3(0, 0, 0);
                onRelease.Invoke();
                isPressed = false;
            }
        }
           
    }
}
