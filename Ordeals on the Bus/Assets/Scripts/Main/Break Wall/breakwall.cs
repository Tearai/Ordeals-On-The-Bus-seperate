using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakwall : MonoBehaviour
{
    public float crashForce = 10;


    public List<GameObject> npc = new List<GameObject>();

    public bool happenOnce;

    public deathBanana banana;

    public GameObject crashSFX;

    public void Start()
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

    public void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("Player"))
        {
            if (happenOnce == false)
            {
                StartCoroutine(bananaAnimation());
                happenOnce = true;
                crashSFX.SetActive(true);
                foreach (GameObject obj in npc)
                {
                    Rigidbody rb = obj.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                        rb.AddForce(transform.right * crashForce, ForceMode.Impulse);
                    }
                }
            }

        }

    }

    IEnumerator bananaAnimation()
    {
        yield return new WaitForSeconds(.3f);
        banana.NPC1Animations.SetBool("isScared", true);
        banana.canSpeak = true;
    }
}
