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
    public GameObject Player;

    [Header("Seats")]
    public bool gotoseat;


    [Header("Animations")]
    public Animator NPC1Animations;
    public GameObject Animation;

    [Header("Special Interaction")]
    public string LeaveAreaName;
    public float WaitTime;
    public npcmovement npc;

    [Header("Dialogue")]
    public GameObject firstDialogue;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Set the initial speed 
        NPC1Animations = Animation.GetComponent<Animator>();


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



    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            transform.LookAt(Player.transform);
            StartCoroutine(Running());
            firstDialogue.SetActive(true);
        }

        if(other.CompareTag("End"))
        {
            Destroy(gameObject);
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
        targetObjectName = LeaveAreaName;
        NPC1Animations.SetBool("isWalk", true);
        NPC1Animations.SetBool("isIdle", false);
        npc.canDriveOff = true;

        float runSpeed = 5f;

        movementSpeed = runSpeed;
    }
}
