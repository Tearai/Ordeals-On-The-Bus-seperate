using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObjects : MonoBehaviour
{
    public List<GameObject> npc = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in npc)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject obj in npc)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
        }
    }
}
