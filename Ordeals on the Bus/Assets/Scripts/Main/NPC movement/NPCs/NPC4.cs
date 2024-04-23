using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC4 : MonoBehaviour
{
    [Header("NPC movement speed")]
    public float movementSpeed = 3.0f; // Public variable for controlling the speed
    private NavMeshAgent navMeshAgent;

    [Header("Going to the driver speed")]
    public float dockingspeed = 5f;
    public string targetObjectName; // Change the target variable to string
    public NPC3 smelly;
    public bool showticket;
    public bool canShow;
    public GameObject Player;

    [Header("Seats")]
    public MeshRenderer ticket;
    public bool ticket4;
    public string[] Seats;
    public bool gotoseat;
    private string randomSeatName;
    public GameObject childObject;
    public GameObject parentObject;

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
    public bool canDriveOff;

    [Header("Ragdoll")]
    private Transform hipBone;
    public string getup;
    public GameObject Bones;
    public Rigidbody[] _ragdollRigidbodies;

    [Header("Dialogue")]
    public GameObject firstDialogue;
    public bool Dialogue1;
    public GameObject SecondDialogue;
    public GameObject ThirdDialogue;
    public bool Dialogue3;
    public GameObject FourthDialogue;
    public bool Dialogue4;
    public GameObject FifthDialogue;
    public bool Dialogue5;
    public GameObject SixthDialogue;
    public GameObject SeventhDialogue;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed; // Set the initial speed 
        NPC1Animations = Animation.GetComponent<Animator>();

        //ragdol 
        hipBone = NPC1Animations.GetBoneTransform(HumanBodyBones.Hips);
        _ragdollRigidbodies = Bones.GetComponentsInChildren<Rigidbody>();

        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }

    }

    void Update()
    {
        if (vip.touchedGround == true)
        {
            navMeshAgent.isStopped = false;
        }


        if (vip.touchedGround == true && !string.IsNullOrEmpty(randomMovementAreaName))
        {
            GameObject randomMovementArea = GameObject.Find(randomMovementAreaName);

            NPC1Animations.SetBool("getup", true);
            NPC1Animations.SetBool("isSit", false);

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



        if (vip.touchedGround == false && gotoseat == false)
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



        if (ticket4 == true)
        {
            gotoseat = true;
            mayham = false;
            NPC1Animations.SetBool("isWalk", true);
            NPC1Animations.SetBool("isIdle", false);
            NPC1Animations.SetBool("isHand", false);
            Dialogue3 = true;
        }

        if (Dialogue3 == true)
        {
            ThirdDialogue.SetActive(true);
            firstDialogue.SetActive(false);
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

        //showing ticket
        showticket = smelly.canGeton;

        if (canShow == true && showticket == true && ticket4 == false)
        {
            ticket.enabled = true;
            NPC1Animations.SetBool("isHand", true);
            firstDialogue.SetActive(false);
            FifthDialogue.SetActive(true);
            SixthDialogue.SetActive(false);
            Dialogue5 = true;
        }
        else
        {
            ticket.enabled = false;
            NPC1Animations.SetBool("isHand", false);
            FifthDialogue.SetActive(false);
        }

        if(canShow == true && showticket == false && Dialogue5 == true)
        {
            SixthDialogue.SetActive(true);
            Dialogue5 = false;
        }


        if (Dialogue4 == true && showticket == true)
        {
            FourthDialogue.SetActive(true);
        }

        if (Dialogue4 == true && showticket == false)
        {
            FourthDialogue.SetActive(false);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            transform.LookAt(Player.transform);
            canShow = true;

            if (Dialogue1 == false)
            {
                firstDialogue.SetActive(true);
                Dialogue1 = true;
            }
        }

        if (other.CompareTag("Hand"))
        {
            ragdoll();
            firstDialogue.SetActive(false);

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
            canShow = false;
        }
    }

    public void leaving()
    {
        MoveTowardsTarget(leavingdestination);
    }

    void GoToRandomSeat()
    {
        if (Seats.Length > 0)
        {
            if (string.IsNullOrEmpty(randomSeatName))
            {
                randomSeatName = Seats[Random.Range(0, Seats.Length)];
            }


            GameObject randomSeat = GameObject.Find(randomSeatName);


            MoveTowardsTarget(randomSeatName); // Pass the string as the target


            if (Vector3.Distance(transform.position, randomSeat.transform.position) < 0.5f)
            {

                transform.eulerAngles = new Vector3(0, 0, 0);
                NPC1Animations.SetBool("isSit", true);
                NPC1Animations.SetBool("isWalk", false);
                NPC1Animations.SetBool("isIdle", false);
                childObject.transform.SetParent(parentObject.transform);

                Dialogue4 = true;
                ThirdDialogue.SetActive(false);
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

    public void ragdoll()
    {
        Vector3 savedPosition = transform.position;
        Quaternion savedRotation = transform.rotation;


        NPC1Animations.enabled = false;
        navMeshAgent.isStopped = true;

        SecondDialogue.SetActive(true);
        StartCoroutine(RestorePositionAndRotation(savedPosition, savedRotation));
        SeventhDialogue.SetActive(false);
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
        SeventhDialogue.SetActive(false);
    }

    public void flinch()
    {
        NPC1Animations.SetBool("isFlinch", true);
        SeventhDialogue.SetActive(true);
        firstDialogue.SetActive(false);
    }

    public void noflinch()
    {
        NPC1Animations.SetBool("isFlinch", false);
    }

}
