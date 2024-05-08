using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC2 : MonoBehaviour
{
    [Header("NPC movement speed")]
    public float movementSpeed = 3.0f; // Public variable for controlling the speed
    private NavMeshAgent navMeshAgent;

    [Header("Going to the driver speed")]
    public float dockingspeed = 5f;
    public string targetObjectName; // Change the target variable to string
    public Transform Player;

    [Header("Seats")]
    public GameObject ticket;
    public bool ticket2;
    public string[] Seats;
    public bool gotoseat;
    private string randomSeatName;
    public GameObject childObject;
    public GameObject parentObject;

    [Header("Look at player")]
    public bool lookat;
    public float rotationSpeed = 5f;

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
    public bool gotHit;

    [Header("Dialogue")]
    public GameObject firstDialogue;
    public bool Dialogue1;
    public GameObject SecondDialogue;
    public GameObject ThirdDialogue;
    public bool Dialogue3;
    public bool walkback;

    [Header("Mouth")]
    public Animator MouthAnim;
    public GameObject Mouth;
    public string Talk1;
    public string Talk2;
    public bool talk2once;


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
            MoveTowardsTarget(targetObjectName);
            NPC1Animations.SetBool("isWalk", true);
            NPC1Animations.SetBool("isIdle", false);

            if (navMeshAgent.remainingDistance < 0.01f)
            {
                NPC1Animations.SetBool("isIdle", true);
                NPC1Animations.SetBool("isWalk", false);
            }
        }



        if (ticket2 == true)
        {
            if (walkback == false)
            {
                if (talk2once == false)
                {
                    MouthAnim.Play(Talk2);
                    talk2once = true;
                }

                StartCoroutine(saythankyou());
                mayham = false;
                NPC1Animations.SetBool("isWalk", false);
                NPC1Animations.SetBool("isIdle", true);
                NPC1Animations.SetBool("isHand", false);

                Dialogue3 = true;

                canRagdoll = false;
            }
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
            navMeshAgent.speed = 0.4f;
        }

        if (lookat)
        {
            transform.LookAt(Player.position);
        }
    }

    IEnumerator saythankyou()
    {
        yield return new WaitForSeconds(13f);
        gotoseat = true;
        NPC1Animations.SetBool("isWalk", true);
        NPC1Animations.SetBool("isIdle", false);
        NPC1Animations.SetBool("isHand", false);
        lookat = false;
        walkback = true;
    }

    public void showticket()
    {
        

        NPC1Animations.SetBool("isHand", true);

        gotHit = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {

            lookat = true;
            ticket.SetActive(true);

            if (gotHit == false)
            {
                Invoke("showticket", 4.0f);
            }

            if (gotHit == true)
            {
                NPC1Animations.SetBool("isHand", true);
            }

            box1.enabled = true;
            if (Dialogue1 == false)
            {
                firstDialogue.SetActive(true);
                MouthAnim.Play(Talk1);
                Dialogue1 = true;
            }
        }

        if (other.CompareTag("Hand"))
        {
            if(canRagdoll == true)
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
                NPC1Animations.SetBool("isChase", false);
                childObject.transform.SetParent(parentObject.transform);

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

    public void flinch()
    {
        NPC1Animations.SetBool("isFlinch", true);
    }

    public void noflinch()
    {
        NPC1Animations.SetBool("isFlinch", false);
    }
}
