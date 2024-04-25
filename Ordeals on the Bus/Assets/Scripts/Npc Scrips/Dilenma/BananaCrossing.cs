    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BananaCrossing : MonoBehaviour
{
    [Header("NPC movement speed")]
    public float movementSpeed = 3.0f; // Public variable for controlling the speed
    private NavMeshAgent navMeshAgent;

    [Header("Going to the driver speed")]
    public float dockingspeed = 5f;
    public string targetObjectName; // Change the target variable to string

    [Header("Going To Location")]
    public bool gotoseat;


    [Header("Animations")]
    public Animator NPC1Animations;
    public GameObject Animation;

    [Header("Special Interaction")]
    public GameObject SplatterVFX;
    public bool GotHit;
    public bool self;

    [Header("Ragdoll")]
    private Transform hipBone;
    public GameObject Bones;
    public Rigidbody[] _ragdollRigidbodies;
    public float hitForce = 500f;

    [Header("SFX")]
    public GameObject Dialogue1;
    public GameObject Dialogue2;
    public GameObject DialogueScream;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Set the initial speed 
        NPC1Animations = Animation.GetComponent<Animator>();

        hipBone = NPC1Animations.GetBoneTransform(HumanBodyBones.Hips);
        _ragdollRigidbodies = Bones.GetComponentsInChildren<Rigidbody>();

        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }

        Dialogue1.SetActive(true);
    }

    void Update()
    {
        navMeshAgent.speed = movementSpeed;

        if (gotoseat == false)
        {
            MoveTowardsTarget(targetObjectName);
            NPC1Animations.SetBool("isWalk", true);
            NPC1Animations.SetBool("isIdle", false);

            if (navMeshAgent.remainingDistance < 0.01f)
            {
                NPC1Animations.SetBool("isIdle", true);
                NPC1Animations.SetBool("isWalk", false);

            }
        }

        if(GotHit == true && self == false)
        {
            navMeshAgent.isStopped = true;
            Dialogue1.SetActive(false);
            DialogueScream.SetActive(true);
            NPC1Animations.SetBool("isIdle", false);
            NPC1Animations.SetBool("isWalk", false);
            NPC1Animations.SetBool("isScared", true);
        }



    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            navMeshAgent.isStopped = true;
            SplatterVFX.SetActive(true);
            GotHit = true;
            self = true;
            NPC1Animations.enabled = false;
            Dialogue1.SetActive(false);
            Dialogue2.SetActive(true);

            foreach (var rigidbody in _ragdollRigidbodies)
            {
                rigidbody.isKinematic = false;
                // Apply force to ragdoll parts to simulate being hit
                rigidbody.AddForce(Vector3.up * hitForce, ForceMode.Impulse);
                rigidbody.AddForce(-transform.right * hitForce, ForceMode.Impulse);
            }
        }
    }

    void MoveTowardsTarget(string targetName)
    {
        if (!string.IsNullOrEmpty(targetName))
        {
            // Find the GameObject with the chosen target name
            GameObject targetObject = GameObject.Find(targetName);

            if (targetObject != null)
            {
                // Use NavMeshAgent for smooth navigation
                navMeshAgent.SetDestination(targetObject.transform.position);
            }
        }
    }
}

