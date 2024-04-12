using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TJunctionLeft : MonoBehaviour
{
    public Animator targetAnimator; // Animator component of the object you want to animate

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider we collided with has a specific tag (you can customize this)
        if (other.CompareTag("Player"))
        {
            // Trigger animation on the targetAnimator
            targetAnimator.SetTrigger("TLeft");
        }
    }
}
