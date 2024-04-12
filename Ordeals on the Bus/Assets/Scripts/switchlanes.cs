using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchlanes : MonoBehaviour
{

    public GameObject bus;
    public GameObject[] lanes; // Use GameObjects for lane positions
    public float speed = 5.0f;
    public int currentLane = 1; // Initialize to the middle lane
    public GameObject parkZone;
    public string busStopName; // Name of the bus stop GameObject

    public bool isEnteringBusStop = false;
    private bool isChangingLane = false;
    public bool canDrive = true;

    public GameObject objectToClick;
    public GameObject objectToClick2;

    public Busmovement stopMove;
    public laneMovetobusStop lanemove;

    private GameObject busStop; // Reference to the bus stop GameObject

    void Start()
    {
        // Find the bus stop GameObject using its name
        busStop = GameObject.Find(busStopName);

        if (busStop == null)
        {
            Debug.LogError("Bus stop GameObject not found with the name: " + busStopName);
        }
    }

    void Update()
    {
        if (isEnteringBusStop == true && isChangingLane == false)
        {
            MoveBusToBusStop();
            return;
        }

        if (!isChangingLane)
        {
            // Check for mouse clicks on objectToClick and objectToClick2
            if (Input.GetMouseButtonDown(0)) // Left mouse button click
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == objectToClick)
                    {
                        SwitchLane(-1); // Move to the left lane
                    }
                    else if (hit.collider.gameObject == objectToClick2)
                    {
                        SwitchLane(1); // Move to the right lane
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            canDrive = true;
            lanemove.enabled = false;
            stopMove.enabled = true;
            currentLane = 0;
            isChangingLane = true;
            StartCoroutine(MoveBus(lanes[currentLane].transform));
        }
    }

    void SwitchLane(int direction)
    {
        int newLane = currentLane + direction;
        if (newLane >= 0 && newLane < lanes.Length)
        {
            currentLane = newLane;
            isChangingLane = true;
            StartCoroutine(MoveBus(lanes[currentLane].transform));
        }
    }

    IEnumerator MoveBus(Transform targetLane)
    {
        Vector3 targetPosition = targetLane.position;

        while (Vector3.Distance(bus.transform.position, targetPosition) > 0.1f)
        {
            targetPosition = targetLane.position; // Update target position to account for moving lanes
            bus.transform.position = Vector3.MoveTowards(bus.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        isChangingLane = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Park"))
        {
            isEnteringBusStop = true;
        }
    }

    void MoveBusToBusStop()
    {
        if (busStop == null)
        {
            Debug.LogError("Bus stop GameObject not found.");
            return;
        }

        // Calculate the direction to the bus stop
        Vector3 directionToBusStop = busStop.transform.position - bus.transform.position;
        directionToBusStop.y = 0; // Make sure it's in the horizontal plane

        // Check if the bus has reached the bus stop
        if (directionToBusStop.magnitude > 0.1f)
        {
            lanemove.enabled = true;
            canDrive = false;
            // Normalize the direction vector and move the bus towards the bus stop
            directionToBusStop.Normalize();
            bus.transform.position += directionToBusStop * speed / (currentLane + 0.1f) * Time.deltaTime;
        }
        else
        {
            // The bus has reached the bus stop
            isEnteringBusStop = false;
            stopMove.enabled = false;

            // You can add additional logic here, such as stopping the bus, opening doors, etc.
        }
    }
}

   