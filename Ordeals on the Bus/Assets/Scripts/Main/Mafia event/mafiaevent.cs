using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mafiaevent : MonoBehaviour
{
    [Header("Good guy")]
    public GameObject Mafia_1;
    public Animator Mafia_1_Animator;
    public GameObject Dialogue1;

    public GameObject Bones;
    public Rigidbody[] _ragdollRigidbodies;
    public GameObject deathSFX1;

    [Header("Bad guy")]
    public GameObject Mafia_2;
    public Animator Mafia_2_Animator;
    public GameObject Dialogue2;

    public GameObject Bones2;
    public Rigidbody[] _ragdollRigidbodies2;
    public GameObject deathSFX2;

    [Header("Orange Gang1")]
    public GameObject Gang1;
    public Animator Gang1Anim;
    public GameObject gunSFX1;

    [Header("Orange Gang2")]
    public GameObject Gang2;
    public Animator Gang2Anim;

    [Header("Orange Gang3")]
    public GameObject Gang3;
    public Animator Gang3Anim;


    [Header("Orange Gang4")]
    public GameObject Gang4;
    public Animator Gang4Anim;

    [Header("Orange Gang5")]
    public GameObject Gang5;
    public Animator Gang5Anim;

    [Header("Orange Gang6")]
    public GameObject Gang6;
    public Animator Gang6Anim;

    // Start is called before the first frame update
    void Start()
    {
        Mafia_1_Animator = Mafia_1.GetComponent<Animator>();
        Mafia_2_Animator = Mafia_2.GetComponent<Animator>();

        Gang1Anim = Gang1.GetComponent<Animator>();

        Gang2Anim = Gang2.GetComponent<Animator>();

        Gang3Anim = Gang3.GetComponent<Animator>();

        Gang4Anim = Gang4.GetComponent<Animator>();

        Gang5Anim = Gang5.GetComponent<Animator>();

        Gang6Anim = Gang6.GetComponent<Animator>();

        //ragdol

        _ragdollRigidbodies = Bones.GetComponentsInChildren<Rigidbody>();

        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }

        _ragdollRigidbodies2 = Bones2.GetComponentsInChildren<Rigidbody>();

        foreach (var rigidbody in _ragdollRigidbodies2)
        {
            rigidbody.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void mafiaTalk()
    {
        Mafia_2_Animator.SetBool("isTalk", true);
    }

    public void mafiaHand()
    {
        Mafia_1_Animator.SetBool("isHand", true);
    }

    public void speak()
    {
        Dialogue1.SetActive(true);
        Dialogue2.SetActive(true);
    }

    public void shoot()
    {
        Gang1Anim.SetBool("Shoot", true);
    }

    public void shooting()
    {
        Gang1Anim.SetBool("Shot", true);
        gunSFX1.SetActive(true);
    }

    public void shoot2()
    {
        Gang2Anim.SetBool("Shoot", true);
    }

    public void shooting2()
    {
        Gang2Anim.SetBool("Shot", true);
    }

    public void shoot3()
    {
        Gang3Anim.SetBool("Shoot", true);
    }

    public void shooting3()
    {
        Gang3Anim.SetBool("Shot", true);
    }

    public void shoot4()
    {
        Gang4Anim.SetBool("Shoot", true);
    }

    public void shooting4()
    {
        Gang4Anim.SetBool("Shot", true);
    }

    public void shoot5()
    {
        Gang5Anim.SetBool("Shoot", true);
    }

    public void shooting5()
    {
        Gang5Anim.SetBool("Shot", true);
    }

    public void shoot6()
    {
        Gang6Anim.SetBool("Shoot", true);
    }

    public void shooting6()
    {
        Gang6Anim.SetBool("Shot", true);
    }

    public void gotshot()
    {
        Mafia_1_Animator.enabled = false;

        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }


    }

    public void gotshot2()
    {
        Mafia_2_Animator.enabled = false;

        foreach (var rigidbody in _ragdollRigidbodies2)
        {
            rigidbody.isKinematic = false;
        }
    }
}
