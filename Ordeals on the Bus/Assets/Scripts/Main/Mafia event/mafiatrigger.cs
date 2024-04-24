using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mafiatrigger : MonoBehaviour
{
    public GameObject Mafia_1;
    public Animator Mafia_1_Animator;




    // Start is called before the first frame update
    void Start()
    {
        Mafia_1_Animator = Mafia_1.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Mafia_1_Animator.enabled = true;
        }
    }
}
