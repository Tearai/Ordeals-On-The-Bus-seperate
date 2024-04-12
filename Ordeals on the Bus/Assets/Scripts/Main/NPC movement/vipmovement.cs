using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class vipmovement : MonoBehaviour
{
    [Header("NPC movement speed")]
    public float movementSpeed = 3.0f; // Public variable for controlling the speed
    private NavMeshAgent navMeshAgent;

    [Header("Going to the driver speed")]
    public float dockingspeed = 5f;
    public string targetObjectName; // Change the target variable to string

    [Header("Seats")]
    public string[] Seats;
    public bool gotoseat;
    public string randomSeatName;

    [Header("Mayham")]
    public bool mayham;
    public string randomMovementAreaName;
    public GameObject icecube;
    public GameObject fire;
    public bool isonFire;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed; // Set the initial speed

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            //mayham = true;
            //gotoseat = false;
            //navMeshAgent.isStopped = false;
            icecube.SetActive(true);
            fire.SetActive(false);
            isonFire = false;

        }
        if (mayham == true && !string.IsNullOrEmpty(randomMovementAreaName))
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

        if (mayham == false && gotoseat == false)
        {
            MoveTowardsTarget(targetObjectName); // Pass the string as the target
        }


        if (Input.GetKey(KeyCode.K))
        {
            gotoseat = true;
            mayham = false;
            fire.SetActive(true);
            isonFire = true;
        }

        if (gotoseat == true && mayham == false)
        {
            GoToRandomSeat();
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
}
