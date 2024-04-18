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
    private bool isGrabbing = false;

    [Header("Going Back")]
    public string goBackLocation;

    [Header("Break Bus")]
    public GameObject BusBack;
    public bool breakOnce;

    [Header("Animations")]
    public Animator gorillaAnimator;

    [Header("SFX")]
    public GameObject breakBusSFX;
    public GameObject swingSFX;
    public GameObject eatSFX;

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
            breakBusSFX.SetActive(true);
        }
    }

    IEnumerator GrabbingBanana()
    {
        print("started");
        Transform npcTransform = npc[currentNPCIndex].transform;
        npcTransform.SetParent(Hand.transform);
        npcTransform.localPosition = Vector3.zero;
        npc[currentNPCIndex].transform.SetParent(Hand.transform);
        yield return new WaitForSeconds(2f);
        Destroy(npc[currentNPCIndex]);
        eatSFX.SetActive(true);
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(2f);
        navMeshAgent.isStopped = false;
        eatSFX.SetActive(false);
        currentNPCIndex++;
        isGrabbing = false;
        yield return null;
    }

    IEnumerator swingSFXs()
    {
        swingSFX.SetActive(true);
        yield return new WaitForSeconds(2f);
        swingSFX.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Grab"))
        {
            if (collisionCount < 2)
            {
                collisionCount++;
            }
                
            Grab = true;
            breakOnce = true;
            gorillaAnimator.SetBool("Swiping", true);
            StartCoroutine(swingSFXs());
            if (collisionCount >=2 && !isGrabbing)
            {
                StartCoroutine(GrabbingBanana());
                isGrabbing = true;
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
