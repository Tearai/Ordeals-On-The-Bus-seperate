using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonTrigger : MonoBehaviour
{
    public Animator TargetAnimator;
    public GameObject TargetObject;

    public GameObject BalloonBoy;
    public Animator BananaAnimator;
    public float speed;
    public bool test;
    public GameObject BalloonBoyRagdoll;
    private Rigidbody[] _ragdollRigidbodies;

    public GameObject ParentB;
    public Animator ParentBAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TargetAnimator = TargetObject.GetComponent<Animator>();
        BananaAnimator = BalloonBoy.GetComponent<Animator>();
        ParentBAnimator = ParentB.GetComponent<Animator>();
        _ragdollRigidbodies = BalloonBoyRagdoll.GetComponentsInChildren<Rigidbody>();

        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(test == true)
        {
            BalloonBoy.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TargetAnimator.enabled = true;
        }
    }

    public void GoUp()
    {
        test = true;
        
    }

    public void kidIdle()
    {
        BananaAnimator.SetBool("yes", true);
    }

    public void GoDown()
    {
        test = false;
        BananaAnimator.enabled = false;
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    public void ParentIdle()
    {
        ParentBAnimator.SetBool("yes", true);
    }
}
