using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GorillaSpawn : MonoBehaviour
{
    public GameObject gorilla;
    public GameObject gorillaMesh;
    public GameObject VFX;
    public bool move;

    public float dropTime;
    public float fallForce = 10.0f;
    public bool canDrop;

    public BoxCollider box;

    public gorillaland land;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        canDrop = land.touchedGround;

        if (canDrop == true)
        {
            StartCoroutine(crashDown());
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody gorillaRb = gorilla.GetComponent<Rigidbody>();
            gorillaRb.useGravity = true;
            gorillaRb.isKinematic = false;
            gorillaMesh.SetActive(true);
            
            
            move = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            move = false;
        }
    }

    IEnumerator crashDown()
    {
        yield return new WaitForSeconds(dropTime);
        Gorilla gorillaComponent = gorilla.GetComponent<Gorilla>();
        gorillaComponent.enabled = true;
        gorilla.GetComponent<Animator>().enabled = true;
        gorilla.GetComponent<NavMeshAgent>().enabled = true;
        VFX.SetActive(true);

        Rigidbody gorillaRb = gorilla.GetComponent<Rigidbody>();
        gorillaRb.useGravity = false;
        gorillaRb.isKinematic = true;
        box.enabled = false;
    }

    void FixedUpdate()
    {
        if (canDrop == false)
        {
            Rigidbody gorillaRb = gorilla.GetComponent<Rigidbody>();
            gorillaRb.AddForce(Vector3.down * fallForce, ForceMode.Acceleration);
        }
    }
}