using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcmovement : MonoBehaviour
{
    [Header("NPC movement speed")]
    public float movementSpeed = 3.0f; // Public variable for controlling the speed
    private NavMeshAgent navMeshAgent;

    [Header("Going to the driver speed")]
    public float dockingspeed = 5f;
    public string targetObjectName; // Change the target variable to string
    public Transform Player;
    public bool canDriveOff;

    [Header("Seats")]
    public GameObject ticket;
    public bool ticket1;
    public string[] Seats;
    public bool gotoseat;
    private string randomSeatName;
    public GameObject childObject;
    public GameObject parentObject;

    [Header("Look at player")]
    public bool lookat;
    public float rotationSpeed = 1f;

    [Header("Mayham")]
    public bool mayham;
    public string randomMovementAreaName;
    public gorillaland vip;

    [Header("Animations")]
    public Animator NPC1Animations;
    public GameObject Animation;

    [Header("Leaving")]
    public string leavingdestination;
    public bool canleave;

    [Header("Ragdoll")]
    private Transform hipBone;
    public string getup;
    public GameObject Bones;
    public Rigidbody[] _ragdollRigidbodies;
    public BoxCollider box1;
    public bool canRagdoll;

    [Header("Dialogue")]
    public GameObject firstDialogue;
    public bool Dialogue1;
    public GameObject SecondDialogue;
    public GameObject ThirdDialogue;
    public bool Dialogue3;
    public bool Dialogue4;
    public GameObject FourthDialogue;

    public GameObject SupervisorDialogue;
    public GameObject SupervisorDialogue2;

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
        navMeshAgent.speed = movementSpeed; // Set the initial speed 
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
        if (vip.touchedGround == true)
        {
            navMeshAgent.isStopped = false;

        }

        if (vip.touchedGround == true && !string.IsNullOrEmpty(randomMovementAreaName))
        {
            

            NPC1Animations.SetBool("isChase", true);
            NPC1Animations.SetBool("isSit", false);

            
        }


        if (vip.touchedGround == false && gotoseat == false)
        {
            MoveTowardsTarget(targetObjectName); // Pass the string as the target
            NPC1Animations.SetBool("isWalk", true);
            NPC1Animations.SetBool("isIdle", false);

            if (navMeshAgent.remainingDistance < 0.1f)  
            {

                NPC1Animations.SetBool("isIdle", true);
                NPC1Animations.SetBool("isWalk", false);
            }

        }


        if (ticket1 == true)
        {
            gotoseat = true;
            mayham = false;
            NPC1Animations.SetBool("isWalk", true);
            NPC1Animations.SetBool("isIdle", false);
            NPC1Animations.SetBool("isHand", false);
            lookat = false;
            navMeshAgent.isStopped = false;

            if (talk3once == false)
            {
                MouthAnim.Play(Talk3);
                talk3once = true;
            }


        }

        if (Dialogue3 == true && ticket1 == false)
        {
            ThirdDialogue.SetActive(true);
            firstDialogue.SetActive(false);
            SupervisorDialogue2.SetActive(true);
            NPC1Animations.SetBool("isWalk", false);
            NPC1Animations.SetBool("isIdle", true);
            NPC1Animations.SetBool("isHand", false);
            canRagdoll = false;
            if (talk2once == false)
            {
                MouthAnim.Play(Talk2);
                talk2once = true;
            }

        }

        if (gotoseat == true && vip.touchedGround == false)
        {
            GoToRandomSeat();
        }

        if (canleave == true)
        {
            targetObjectName = leavingdestination;
            NPC1Animations.SetBool("getup", true);
            NPC1Animations.SetBool("isSit", false);
        }

        if (lookat)
        {
            transform.LookAt(Player.position);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            ticket.SetActive(true);
            NPC1Animations.SetBool("isHand", true);
            box1.enabled = true;
            lookat = true;

            //sound
            if (Dialogue1 == false)
            {
                firstDialogue.SetActive(true);
                Dialogue1 = true;
                SupervisorDialogue.SetActive(true);
                MouthAnim.Play(Talk1);
            }

        }

        if (other.CompareTag("Hand"))
        {
            if (canRagdoll == true)
            {
                ticket.SetActive(false);
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
            NPC1Animations.SetBool("isHand", false);
            lookat = false;

        }
    }


    /// <summary>
    /// Picking a random seat
    /// </summary>
    void GoToRandomSeat()
    {
        if (Seats.Length > 0)
        {
            if (string.IsNullOrEmpty(randomSeatName))
            {
                // Choose a random seat name from the array for the first time
                randomSeatName = Seats[Random.Range(0, Seats.Length)];
            }

            // Find the GameObject with the chosen seat name
            GameObject randomSeat = GameObject.Find(randomSeatName);

            // Move towards the selected seat
            MoveTowardsTarget(randomSeatName); // Pass the string as the target

            // Check if the NPC has reached the seat
            if (Vector3.Distance(transform.position, randomSeat.transform.position) < 0.5f)
            {
                // Set the y-rotation to 0
                transform.eulerAngles = new Vector3(0, 0, 0);
                NPC1Animations.SetBool("isSit", true);
                NPC1Animations.SetBool("isWalk", false);
                NPC1Animations.SetBool("isIdle", false);
                NPC1Animations.SetBool("getup", false);
                NPC1Animations.SetBool("isChase", false);
                childObject.transform.SetParent(parentObject.transform);
                ThirdDialogue.SetActive(false);

                //FourthDialogue.SetActive(true);

                // Optional: You can disable the NavMeshAgent once the NPC reaches the seat
                //navMeshAgent.isStopped = true;

                // Optional: Set gotoseat to false or perform other actions as needed
                //gotoseat = false;
            }
        }
        else
        {
            Debug.LogWarning("No seat names defined in the array.");
        }
    }

    /// <summary>
    /// Making the npc go to the driver
    /// </summary>
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
            else
            {
                Debug.LogWarning("Target object not found.");
            }
        }
    }

    /// <summary>
    /// Random movements of the NPC
    /// </summary>
    void PerformRandomMovementInArea(Vector3 minBounds, Vector3 maxBounds)
    {
        // Check if the NavMeshAgent has reached the current destination
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            // Generate a random point within the specified rectangular area
            Vector3 randomDestination = RandomNavInBounds(minBounds, maxBounds);

            // Set the destination for the NavMeshAgent
            navMeshAgent.SetDestination(randomDestination);
        }
    }

    Vector3 RandomNavInBounds(Vector3 minBounds, Vector3 maxBounds)
    {
        Vector3 randomDirection = new Vector3(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y),
            Random.Range(minBounds.z, maxBounds.z)
        );

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, 5f, -1);

        return navHit.position;
    }

    public void flinch()
    {
        NPC1Animations.SetBool("isFlinch", true);
    }

    public void noflinch()
    {
        NPC1Animations.SetBool("isFlinch", false);
    }

    public void ragdoll()
    {
        Vector3 savedPosition = transform.position;
        Quaternion savedRotation = transform.rotation;


        NPC1Animations.enabled = false;
        navMeshAgent.isStopped = true;

        SecondDialogue.SetActive(true);
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
        SecondDialogue.SetActive(false);
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }
}