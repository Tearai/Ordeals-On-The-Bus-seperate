using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC3 : MonoBehaviour
{
    [Header("NPC movement speed")]
    public float movementSpeed = 3.0f;
    private NavMeshAgent navMeshAgent;

    [Header("Going to the driver speed")]
    public float dockingspeed = 5f;
    public string targetObjectName;
    public Transform Player;

    [Header("Seats")]
    public GameObject ticket;
    public bool ticket3;
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

    [Header("Animations")]
    public string leavingdestination;
    public bool canleave;

    [Header("Special Interaction")]
    public GameObject smelly;


    public Material material;
    public Color startColor;
    public Color endColor;
    public float duration = 2f;
    public bool colorchange;
    public bool canGeton;
    public bool cleaning;

    private float startTime;

    [Header("Ragdoll")]
    private Transform hipBone;
    public string getup;
    public GameObject Bones;
    public Rigidbody[] _ragdollRigidbodies;
    public bool canRagdoll;
    public BoxCollider box1;

    [Header("Dialogue")]
    public GameObject firstDialogue;
    public GameObject SecondDialogue;
    public bool Dialogue1;
    public GameObject ThirdDialogue;
    public bool Dialogue3;
    public GameObject FourthDialogue;
    public GameObject FifthDialogue;
    public bool gotHit;
    public bool walkback;

    [Header("Mouth")]
    public Animator MouthAnim;
    public GameObject Mouth;
    public string Talk1;
    public string Talk2;
    public bool talk2once;
    public string Talk3;
    public bool talk3once;
    public string Talk4;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed; // Set the initial speed 
        NPC1Animations = Animation.GetComponent<Animator>();

        //color change 
        startTime = Time.time;

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



        if (ticket3 == true)
        {
            if (walkback == false)
            {
                StartCoroutine(saythankyou());
                mayham = false;
                NPC1Animations.SetBool("isWalk", false);
                NPC1Animations.SetBool("isIdle", true);
                NPC1Animations.SetBool("isHand", false);

                Dialogue3 = true;

                canRagdoll = false;

                if (talk3once == false)
                {
                    MouthAnim.Play(Talk3);
                    talk3once = true;
                }
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
        }

        //color change
        if (colorchange == true)
        {
            StartCoroutine(ChangeBackColorOverTime());
        }

        if (lookat)
        {
            transform.LookAt(Player.position);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            smelly.SetActive(false);

            if (cleaning == false)
            {
                StartCoroutine(ChangeColorOverTime());
            }
            MouthAnim.Play(Talk4);
            FourthDialogue.SetActive(false);
            FifthDialogue.SetActive(true);

        }

        if (other.CompareTag("Finish"))
        {
            lookat = true;
            ticket.SetActive(true);
            if (gotHit == false)
            {
                Invoke("showticket", 7.0f);
            }


            if (gotHit == true)
            {
                NPC1Animations.SetBool("isHand", true);
            }

            box1.enabled = true;
            if (Dialogue1 == false)
            {
                MouthAnim.Play(Talk1);
                firstDialogue.SetActive(true);
                Dialogue1 = true;
            }
        }

        if (other.CompareTag("Hand"))
        {
            if (canRagdoll == true)
            {
                ticket.SetActive(false);
                MouthAnim.Play(Talk2);
                gotHit = true;
                box1.enabled = false;
                ragdoll();
                firstDialogue.SetActive(false);

                foreach (var rigidbody in _ragdollRigidbodies)
                {
                    rigidbody.isKinematic = false;
                }


            }


        }
    }

    IEnumerator saythankyou()
    {
        yield return new WaitForSeconds(3f);
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
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            NPC1Animations.SetBool("isHand", false);
            lookat = false;
        }
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
                //FourthDialogue.SetActive(true);
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

    //color change
    private IEnumerator ChangeColorOverTime()
    {
        cleaning = true;
        canGeton = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Color lerpedColor = Color.Lerp(startColor, endColor, t);
            material.color = lerpedColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        material.color = endColor;
        yield return new WaitForSeconds(17f);
        colorchange = true;

        FourthDialogue.SetActive(false);
        FifthDialogue.SetActive(false);
    }

    private IEnumerator ChangeBackColorOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Color lerpedColor = Color.Lerp(endColor, startColor, t);
            material.color = lerpedColor;

            elapsedTime += Time.deltaTime;
            yield return null;
            canGeton = false;
        }

        material.color = startColor;

        smelly.SetActive(true);
        colorchange = false;

        yield return new WaitForSeconds(0.5f);

        cleaning = false;
        FourthDialogue.SetActive(true);
        MouthAnim.Play(Talk4);
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
