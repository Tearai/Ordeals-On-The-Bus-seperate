using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Breakbusback : MonoBehaviour
{
    public GameObject Sceneloader;
    public bool MonkeyCatch = false;
    public bool Eat = false;
    private Vector3 originalPosition;
    private float moveSpeed = 1099f;
    private Rigidbody rb;
    private float resetter;
    public GameObject NPC1;
    public GameObject NPC2;
    public GameObject NPC3;
    public GameObject NPC4;
    public GameObject NPC5;
    public float eater = 0;
    Animator animators;
   

    void Start()
    {
        animators = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found. Attach a Rigidbody to the monkey GameObject.");
        }
        originalPosition = transform.position;
    }

    public void EnableEat()
    {
        
        
        Eat = true;
        StartCoroutine((IEnumerator)Eatdelay());

    }

    void Update()
    {
        if(Eat == true)
        {
            resetter++;
           
        }
        if (resetter == 2f)
        {
            Eat = false;
            resetter = 0;
        }
        if (Eat == true && rb != null)
        {
            // Move forward using Rigidbody
            rb.MovePosition(rb.position + Vector3.forward * moveSpeed * Time.deltaTime);


            // Log current position for debugging
           // Debug.Log("Current Position: " + transform.position);
            Debug.Log("eat" + Eat);

        }
        if (Eat == false)
        {
            StartCoroutine(ReturnToOriginalPosition());
        }
        Debug.Log("eat" + Eat);
    }
    IEnumerator ReturnToOriginalPosition()
    {
        // Wait for a short delay
        yield return new WaitForSeconds(3f);

        // Log original position for debugging
        Debug.Log("Original Position: " + originalPosition);

        // Continuously move back to the original position
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            rb.MovePosition(Vector3.Lerp(rb.position, originalPosition, moveSpeed * Time.deltaTime));
            yield return null;
        }

        // Reset the Eat flag
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Break"))
        {
            Debug.Log("works");
            Destroy(other.gameObject);
        }
        
            if (Eat == true)
            {

                if (eater == 1 && NPC1 != null)
                {
                    animators.SetTrigger("Swiping");
                    Debug.Log("hes Eaten 1");
                    Destroy(NPC1);
                }
                if (eater == 1 && NPC1 == null)
                {
                    eater = 2;
                }

                if (eater == 2 && NPC2 != null)
                {
                    animators.SetTrigger("Swiping");
                    Debug.Log("hes Eaten 1");
                    Destroy(NPC2);
                }
                if (eater == 2 && NPC2 == null)
                {
                    eater = 3;
                }
                if (eater == 3 && NPC3 != null)
                {
                    animators.SetTrigger("Swiping");
                    Debug.Log("hes Eaten 1");
                    Destroy(NPC3);
                }
                if (eater == 3 && NPC3 == null)
                {
                    eater = 4;
                }

                if (eater == 4 && NPC4 != null)
                {
                    animators.SetTrigger("Swiping");
                    Debug.Log("hes Eaten 1");
                    Destroy(NPC4);
                }
                if (eater == 4 && NPC4 == null)
                {
                    eater = 5;
                }

                if (eater == 5 && NPC5 != null)
                {
                    animators.SetTrigger("Swiping");
                    Debug.Log("hes Eaten 1");
                    Destroy(NPC5);
                }
                if (eater >= 5 && NPC5 == null)
                {
                    animators.SetTrigger("Swiping");
                    StartCoroutine(Deathdelay());
                }

            
           
        }
    }
    IEnumerator Deathdelay()
    {

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(2);
    }
    IEnumerable Eatdelay()
    {
        eater += 1;
        yield return new WaitForSeconds(5);
    }


}