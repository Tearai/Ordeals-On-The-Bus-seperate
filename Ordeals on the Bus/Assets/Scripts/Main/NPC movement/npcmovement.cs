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
    public GameObject Player;
    public bool canDriveOff;

    [Header("Seats")]
    public MeshRenderer ticket;
    public bool ticket1;
    public string[] Seats;
    public bool gotoseat;
    private string randomSeatName;
    public GameObject childObject;
    public GameObject parentObject;

    [Header("Mayham")]
    public bool mayham;
    public string randomMovementAreaName;
    public vipmovement vip;

    [Header("Animations")]
    public Animator NPC1Animations;
    public GameObject Animation;

    [Header("Animations")]
    public string leavingdestination;
    public bool canleave;

    [Header("Ragdoll")]
    private Transform hipBone;
    public string getup;
    public GameObject Bones;
    public Rigidbody[] _ragdollRigidbodies;

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
    }

    void Update()
    {
        if (vip.isonFire == true)
        {


            navMeshAgent.isStopped = false;
        }
        if (vip.isonFire == true && !string.IsNullOrEmpty(randomMovementAreaName))
        {
            GameObject randomMovementArea = GameObject.Find(randomMovementAreaName);

            if (randomMovementArea != null)
            {
                // Get the bounds of the BoxCollider
                Vector3 minBounds = randomMovementArea.GetComponent<Collider>().bounds.min;
                Vector3 maxBounds = randomMovementArea.GetComponent<Collider>().bounds.max;

                PerformRandomMovementInArea(minBounds, maxBounds);
            }
            else
            {
                Debug.LogWarning("Random movement area not found.");
            }
        }

        if (vip.isonFire == false && gotoseat == false)
        {
            MoveTowardsTarget(targetObjectName); // Pass the string as the target
            NPC1Animations.SetBool("isWalk", true);
            NPC1Animations.SetBool("isIdle", false);

            if (navMeshAgent.remainingDistance < 0.01f)
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

        }

        if (gotoseat == true && vip.isonFire == false)
        {
            GoToRandomSeat();
        }

        if(canleave == true)
        {
            targetObjectName = leavingdestination;
            NPC1Animations.SetBool("getup", true);
            NPC1Animations.SetBool("isSit", false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            ticket.enabled = true;
            transform.LookAt(Player.transform);
            NPC1Animations.SetBool("isHand", true);
        }

        if (other.CompareTag("Hand"))
        {
            ragdoll();

            foreach (var rigidbody in _ragdollRigidbodies)
            {
                rigidbody.isKinematic = false;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            NPC1Animations.SetBool("isHand", false);
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
                childObject.transform.SetParent(parentObject.transform);



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
    }
}