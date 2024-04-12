using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaSpawn : MonoBehaviour
{
    public GameObject gorilla;
    public GameObject gorillaMesh;
    public GameObject VFX;
    public bool move;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gorilla.GetComponent<Gorilla>().enabled = true;
            gorilla.GetComponent<Animator>().enabled = true;
            VFX.SetActive(true);
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
}
