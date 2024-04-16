using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC5 : MonoBehaviour
{
    [Header("NPC movement speed")]
    public float movementSpeed = 3.0f; // Public variable for controlling the speed
    private NavMeshAgent navMeshAgent;

    [Header("Going to the driver speed")]
    public float dockingspeed = 5f;
    public string targetObjectName; // Change the target variable to string
    public GameObject Player;

    [Header("Seats")]
    public MeshRenderer ticket;
    public bool ticket5;
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

    [Header("Leaving")]
    public string leavingdestination;
    public bool canleave;

    [Header("Special Interaction")]
    public bool smoking;
    public GameObject smokingVFX;
    public ParticleSystem smoke;
    public bool canSmoke;

    [Header("Dialogue")]
    public GameObject firstDialogue;
    public bool Dialogue1;
    public GameObject ThirdDialogue;
    public bool Dialogue3;
    public GameObject FourthDialogue;
    public bool Dialogue4;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed; // Set the initial speed 
        NPC1Animations = Animation.GetComponent<Animator>();

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
            MoveTowardsTarget(targetObjectName);
            NPC1Animations.SetBool("isWalk", true);
            NPC1Animations.SetBool("isIdle", false);

            if (navMeshAgent.remainingDistance < 0.01f)
            {
                NPC1Animations.SetBool("isIdle", true);
                NPC1Animations.SetBool("isWalk", false);
            }
        }



        if (ticket5 == true)
        {
            gotoseat = true;
            mayham = false;
            NPC1Animations.SetBool("isWalk", true);
            NPC1Animations.SetBool("isIdle", false);

            Dialogue3 = true;
        }

        if (Dialogue3 == true)
        {
            ThirdDialogue.SetActive(true);
            firstDialogue.SetActive(false);
        }

        if (gotoseat == true && vip.isonFire == false)
        {
            GoToRandomSeat();
        }

        if (canleave == true)
        {
            targetObjectName = leavingdestination;
            NPC1Animations.SetBool("getup", true);
            NPC1Animations.SetBool("isSit", false);
        }

        //smoke
        if(canSmoke == true)
        {
            var main = smoke.main;
            main.loop = false;
            FourthDialogue.SetActive(false);
        }

        if (smoking == true)
        {
            var main = smoke.main;
            main.loop = true;
            smoke.Play();
            FourthDialogue.SetActive(true);

        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            transform.LookAt(Player.transform);
            ticket.enabled = true;

            if (Dialogue1 == false)
            {
                firstDialogue.SetActive(true);
                Dialogue1 = true;
            }
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
                smokingVFX.SetActive(true);
                smokingVFX.transform.parent = null;

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
}