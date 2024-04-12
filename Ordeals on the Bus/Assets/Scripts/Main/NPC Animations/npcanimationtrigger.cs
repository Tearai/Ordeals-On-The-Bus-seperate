using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcanimationtrigger : MonoBehaviour
{
    public Animator TargetAnimator;
    public GameObject TargetObject;
    // Start is called before the first frame update
    void Start()
    {
        TargetAnimator = TargetObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        TargetAnimator.enabled = true;
    }
}
