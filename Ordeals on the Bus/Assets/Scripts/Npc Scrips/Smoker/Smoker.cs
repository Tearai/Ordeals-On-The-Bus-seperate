using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoker : MonoBehaviour
{
    public GameObject particleSystemObject;
    private ParticleSystem particleSystemComponent;
    public bool enableParticles ;
    

    void Start()
    {
        // Get the ParticleSystem component from the GameObject
        particleSystemComponent = particleSystemObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Check if the boolean variable is true
        if (enableParticles)
        {
            // Enable the particle system
            particleSystemComponent.Play();
        }
        else
        {
            // Disable the particle system
            particleSystemComponent.Stop();
        }
       
    }
     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cleaner"))
            {
            enableParticles = false;
            StartCoroutine(SmokeAgain());
        }
    }
    IEnumerator SmokeAgain()
    {

        yield return new WaitForSeconds(Random.Range(15f,20f));
        enableParticles = true;
    }
}
