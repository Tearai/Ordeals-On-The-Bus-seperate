using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gorilla : MonoBehaviour
{
    [Header("Look Foward")]
    public GameObject Player;

    [Header("Grab Location")]
    private NavMeshAgent navMeshAgent;
    public string targetObjectName;
    public float movementSpeed;
    public bool Grab;
    public List<GameObject> npc = new List<GameObject>();
    public int currentNPCIndex = 0;

    [Header("Grabbing")]
    public GameObject Hand;
    public int collisionCount = 0;

    [Header("Going Back")]
    public string goBackLocation;

    [Header("Break Bus")]
    public GameObject BusBack;
    public bool breakOnce;

    [Header("Animations")]
    public Animator gorillaAnimator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        gorillaAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);
        navMeshAgent.speed = movementSpeed;


        if (Grab == true)
        {
            MoveToGrabArea(goBackLocation);
            movementSpeed = 2;
            
        }

        if(Grab == false)
        {
            MoveToGrabArea(targetObjectName);
            movementSpeed = 5;
        }

        if(breakOnce == true)
        {
            Destroy(BusBack);
        }
    }

    IEnumerator GrabbingBanana()
    {
        Transform npcTransform = npc[currentNPCIndex].transform;
        npcTransform.SetParent(Hand.transform);
        npcTransform.localPosition = Vector3.zero;
        npc[currentNPCIndex].transform.SetParent(Hand.transform);
        yield return new WaitForSeconds(2f);
        Destroy(npc[currentNPCIndex]);
        yield return new WaitForSeconds(2f);
        currentNPCIndex++;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Grab"))
        {
            collisionCount++;
            Grab = true;
            breakOnce = true;
            gorillaAnimator.SetBool("Swiping", true);
            if(collisionCount >=2)
            {
                StartCoroutine(GrabbingBanana());
            }
        }

        if(other.CompareTag("Respawn"))
        {
            Die();
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Grab"))
        {
            gorillaAnimator.SetBool("Swiping", false);
        }
    }

    public void MoveToGrabArea(string targetName)
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

    public void Die()
    {
        Destroy(gameObject);
    }
}
