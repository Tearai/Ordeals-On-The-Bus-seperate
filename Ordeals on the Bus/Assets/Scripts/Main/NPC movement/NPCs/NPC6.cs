using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC6 : MonoBehaviour
{
    [Header("NPC movement speed")]
    public float movementSpeed = 3.0f; // Public variable for controlling the speed
    private NavMeshAgent navMeshAgent;

    [Header("Going to the driver speed")]
    public float dockingspeed = 5f;
    public string targetObjectName; // Change the target variable to string
    public Transform Player;

    [Header("Seats")]
    public bool gotoseat;

    [Header("Look at player")]
    public bool lookat;
    public float rotationSpeed = 1f;

    [Header("Animations")]
    public Animator NPC1Animations;
    public GameObject Animation;

    [Header("Special Interaction")]
    public bool run;
    public string LeaveAreaName;

    [Header("Dialogue")]
    public GameObject firstDialogue;
    public bool dialogue1;
    public GameObject secondDialogue;
    public GameObject GotHitDialogue;
    public GameObject SupervisorReaction;
    public bool gotmade;
    public GameObject supervisorDialogue;

    [Header("Ragdoll")]
    private Transform hipBone;
    public string getup;
    public GameObject Bones;
    public Rigidbody[] _ragdollRigidbodies;
    public BoxCollider box1;
    public bool canRagdoll;
    public GameObject mush1;
    public GameObject mush2;

    [Header("Mouth")]
    public Animator MouthAnim;
    public GameObject Mouth;
    public string Talk1;
    public string Talk2;
    public bool talk2once;
    


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

        canRagdoll = true;

        MouthAnim = Mouth.GetComponent<Animator>();

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

        if (run == true)
        {
            firstDialogue.SetActive(false);
            secondDialogue.SetActive(true);
            supervisorDialogue.SetActive(true);


            SupervisorReaction.SetActive(false);

            if (talk2once == false)
            {
                MouthAnim.Play(Talk2);
                talk2once = true;
            }
        }

        if (gotmade)
        {
            targetObjectName = LeaveAreaName;
            navMeshAgent.speed = 5f;
            NPC1Animations.SetBool("isWalk", false);
            NPC1Animations.SetBool("isIdle", false);
            NPC1Animations.SetBool("isRun", true);
        }

        if (lookat)
        {
            transform.LookAt(Player.position);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            lookat = true;
            if (dialogue1 == false)
            {
                firstDialogue.SetActive(true);
                SupervisorReaction.SetActive(true);
                dialogue1 = true;
                mush1.SetActive(false);
                mush2.SetActive(true);
                MouthAnim.Play(Talk1);
            }

        }

        if (other.CompareTag("End"))
        {
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Hand"))
        {
            if (canRagdoll == true)
            {
                ragdoll();
                firstDialogue.SetActive(false);

                foreach (var rigidbody in _ragdollRigidbodies)
                {
                    rigidbody.isKinematic = false;
                }
                box1.enabled = false;

            }


        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            lookat = false;
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

    public void ragdoll()
    {
        Vector3 savedPosition = transform.position;
        Quaternion savedRotation = transform.rotation;


        NPC1Animations.enabled = false;
        navMeshAgent.isStopped = true;

        GotHitDialogue.SetActive(true);
        StartCoroutine(RestorePositionAndRotation(savedPosition, savedRotation));
    }

    IEnumerator RestorePositionAndRotation(Vector3 position, Quaternion rotation)
    {
        //NPC1Animations.SetBool("isStand", true);
        NPC1Animations.Play(getup);


        yield return new WaitForSeconds(5f);

        Vector3 originalHipsPosition = hipBone.position;
        transform.position = hipBone.position;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
        }

        hipBone.position = originalHipsPosition;


        NPC1Animations.enabled = true;

        yield return new WaitForSeconds(2.5f);

        NPC1Animations.SetBool("isStand", false);



        yield return new WaitForSeconds(2.5f);
        navMeshAgent.isStopped = false;
        GotHitDialogue.SetActive(false);
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }

}
