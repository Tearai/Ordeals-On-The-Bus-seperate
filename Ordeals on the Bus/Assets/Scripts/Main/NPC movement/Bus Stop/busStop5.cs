using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class busStop5 : MonoBehaviour
{
    public bool busstoping;
    worldmove4 busstop;
    public dockcheck3 dock1;

    public GameObject busdoor;
    public Animator busdoorAnim;
    public string animationName;
    public npcspawnbool4 inZone;

    // Start is called before the first frame update
    void Start()
    {
        busstop = GameObject.FindGameObjectWithTag("World").GetComponent<worldmove4>();
        busdoorAnim = busdoor.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dock1.dockingmode = false;
            busstoping = true;
            busstop.speed = 0;

        }
    }

    public void GetOn()
    {
        if (inZone.businzone4 == true)
        {
            busdoorAnim.Play(animationName);
        }
    }
}
