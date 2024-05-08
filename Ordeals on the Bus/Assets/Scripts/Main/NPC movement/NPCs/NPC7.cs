using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC7 : MonoBehaviour
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
    public string LeaveAreaName;
    public float WaitTime;
    public npcmovement npc;
    public bool canRun;
    public bool isRun;

    [Header("Dialogue")]
    public GameObject firstDialogue;
    public bool Dialogue1;
    public GameObject SecondDialogue;
    public GameObject ThirdDialogue;
    public GameObject SupervisorDialogue;
    public GameObject SupervisorDialogue2;
    public GameObject SupervisorDialogue3;

    [Header("Ragdoll")]
    private Transform hipBone;
    public string getup;
    public GameObject Bones;
    public Rigidbody[] _ragdollRigidbodies;
    public bool canRagdoll;

    [Header("Mouth")]
    public Animator MouthAnim;
    public GameObject Mouth;
    public string Talk1;
    public string Talk2;
    public bool talk2once;
    public string Talk3;
    public bool talk3once;

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

        if (canRun == false && isRun == true)
        {
            targetObjectName = LeaveAreaName;
            NPC1Animations.SetBool("isWalk", false);
            NPC1Animations.SetBool("isIdle", false);
            NPC1Animations.SetBool("isRun", true);
            npc.canDriveOff = true;
            navMeshAgent.isStopped = false;
            float runSpeed = 5f;

            movementSpeed = runSpeed;
        }

        if (talk2once == true && talk3once == false && canRun == false)
        {
            SecondDialogue.SetActive(true);
            SupervisorDialogue2.SetActive(true);
            MouthAnim.Play(Talk2);
            talk3once = true;
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
            StartCoroutine(Running());
            lookat = true;

            if (Dialogue1 == false)
            {
                firstDialogue.SetActive(true);
                Dialogue1 = true;
                SupervisorDialogue.SetActive(true);
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
                MouthAnim.Play(Talk3);
                ragdoll();
                firstDialogue.SetActive(false);
                SupervisorDialogue.SetActive(false);

                foreach (var rigidbody in _ragdollRigidbodies)
                {
                    rigidbody.isKinematic = false;
                }

                ThirdDialogue.SetActive(true);

                canRun = true;
                SupervisorDialogue3.SetActive(true);
                targetObjectName = LeaveAreaName;
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

    IEnumerator Running()
    {
        yield return new WaitForSeconds(WaitTime);
        talk2once = true;
        canRagdoll = false;
        yield return new WaitForSeconds(4f);
        isRun = true;

    }

    public void ragdoll()
    {
        Vector3 savedPosition = transform.position;
        Quaternion savedRotation = transform.rotation;


        NPC1Animations.enabled = false;
        navMeshAgent.isStopped = true;

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
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }

        yield return new WaitForSeconds(.5f);
        NPC1Animations.SetBool("isWalk", false);
        NPC1Animations.SetBool("isIdle", false);
        NPC1Animations.SetBool("isRun", true);
        npc.canDriveOff = true;

        float runSpeed = 5f;

        movementSpeed = runSpeed;
    }
}
