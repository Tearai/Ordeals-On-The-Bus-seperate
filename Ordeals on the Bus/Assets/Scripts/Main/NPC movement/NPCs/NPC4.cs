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
    public Transform Player;

    [Header("Seats")]
    public GameObject ticket;
    public bool ticket4;
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
    public bool canDriveOff;

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
    public GameObject FourthDialogue;
    public bool Dialogue4;
    public GameObject FifthDialogue;
    public bool Dialogue5;
    public GameObject SixthDialogue;
    public GameObject SeventhDialogue;
    public GameObject MustacheDialogue;
    public bool isSit;
    public bool isFlinching;
    public bool canComplain;
    public GameObject Supervisorreaction;
    public bool gorillaLanded;
    public GameObject supervisorAD;

    [Header("Spawn gun")]
    public GameObject gun;
    public adtrigger ads;
    public Transform spawnPoint;

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

        //ragdol 
        hipBone = NPC1Animations.GetBoneTransform(HumanBodyBones.Hips);
        _ragdollRigidbodies = Bones.GetComponentsInChildren<Rigidbody>();

        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        canRagdoll = true;

        MustacheDialogue.SetActive(true);

        MouthAnim = Mouth.GetComponent<Animator>();

        MouthAnim.Play(Talk1);
    }

    void Update()
    {
        if (vip.touchedGround == true)
        {
            navMeshAgent.isStopped = false;
            gorillaLanded = true;
            FourthDialogue.SetActive(false);
        }


        if (vip.touchedGround == true && !string.IsNullOrEmpty(randomMovementAreaName))
        {
            GameObject randomMovementArea = GameObject.Find(randomMovementAreaName);

            NPC1Animations.SetBool("isChase", true);
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
            lookat = false;
            canRagdoll = false;
            isSit = true;
        }

        if (Dialogue3 == true)
        {
            //ThirdDialogue.SetActive(true);
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

        if (canShow == true && showticket == true && ticket4 == false && isSit == false)
        {
            ticket.SetActive(true);
            NPC1Animations.SetBool("isHand", true);
            firstDialogue.SetActive(false);
            FifthDialogue.SetActive(true);
            SixthDialogue.SetActive(false);
            Supervisorreaction.SetActive(false);
            Dialogue5 = true;
            MouthAnim.Play(Talk3);
        }
        else
        {
            ticket.SetActive(false);
            NPC1Animations.SetBool("isHand", false);
            FifthDialogue.SetActive(false);
        }

        if (canShow == true && showticket == false && Dialogue5 == true)
        {
            SixthDialogue.SetActive(true);
            Dialogue5 = false;
        }


        if (canComplain == true && showticket == true && gorillaLanded == false)
        {
            FourthDialogue.SetActive(false);
        }

        if (canComplain == true && showticket == false && gorillaLanded == false)
        {
            FourthDialogue.SetActive(true);
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
            canShow = true;

            if (Dialogue1 == false)
            {
                firstDialogue.SetActive(true);
                Dialogue1 = true;
                MustacheDialogue.SetActive(false);
                Supervisorreaction.SetActive(true);
                MouthAnim.Play(Talk2);
                StartCoroutine(spawngun());
            }
            box1.enabled = true;
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
            canShow = false;
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
                FifthDialogue.SetActive(false);
                isSit = true;

                Dialogue4 = true;
                canComplain = true;
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

        if (isFlinching == false)
        {
            StartCoroutine(flinchDialoguetimer());
        }

    }

    public void noflinch()
    {
        NPC1Animations.SetBool("isFlinch", false);
    }


    IEnumerator flinchDialoguetimer()
    {
        isFlinching = true;
        yield return new WaitForSeconds(3f);
        SeventhDialogue.SetActive(false);
        isFlinching = false;

    }

    IEnumerator spawngun()
    {
        yield return new WaitForSeconds(25f);
        ads.spawnads();
        yield return new WaitForSeconds(10f);
        GameObject spawnedGun = Instantiate(gun, spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(5f);
        supervisorAD.SetActive(true);
    }

}
