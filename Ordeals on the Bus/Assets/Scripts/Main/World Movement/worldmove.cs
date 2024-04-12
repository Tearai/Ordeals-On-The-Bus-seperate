using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;


public class worldmove : MonoBehaviour
{
    public XRSlider slider;
    public XRKnob wheel;
    public float wheelangle;
    // Variables for world movement
    float minSpeed = 1f; // Minimum speed
    float maxSpeed = 5f; // Maximum speed

    // Variables for world movement
    public float speed = 1;
    public bool candrive;
    public bool canspeed;

    // Variables for parking
    public float targetXPosition = 114.32f;
    public float moveSpeed = 1f;
    public bool canPark;

    //Speed
    public float finalspeed;

    // Reference to stopbus script
    stopbus stoppingbus;

    // Variables for going forward
    public float gobackPosition = 109.42f;

    // Variables for switching lanes
    public GameObject leftButton;
    public GameObject rightButton;
    public float[] lanes;
    public int currentLaneIndex = 1;
    public bool isSwitchingLane = false;

    // Variables for bus leaving
    public npcmovement npcleave;
    public NPC3 npcleave2;

    public GameObject busdoor;
    public Animator busdoorAnim;
    public string animationName;

    // Collision with stationary objects
    bool worldStopped = false;
    float originalSpeed;
    float worldStopTimer = 0f;
    public float newSpeed;
    public float sliderValue;


    void Start()
    {
        stoppingbus = GameObject.FindGameObjectWithTag("Stop").GetComponent<stopbus>();
        busdoorAnim = busdoor.GetComponent<Animator>();
        canspeed = true;
        UpdateBusPosition();
    }

    void Update()
    {
        //Wheel value
        wheelangle = wheel.value;

        if(wheelangle == 1)
        {
            Right();
        }

        if (wheelangle == 0)
        {
            Left();
        }

        // Adjust speed based on slider value
        sliderValue = slider.value; // Get slider value between 0 and 1

        newSpeed = Mathf.Lerp(minSpeed, maxSpeed, sliderValue); // Interpolate speed between min and max based on slider value

        finalspeed = newSpeed * speed;

        drive(finalspeed); // Drive with the new speed

        //bus driving
        //drive();

        //Switching lane


        // Check if the world is stopped
        if (worldStopped)
        {
            // Reduce the timer for world stoppage
            worldStopTimer -= Time.deltaTime;

            // Check if the timer has expired
            if (worldStopTimer <= 0f)
            {
                // Resume original speed
                speed = originalSpeed;
                worldStopped = false;
            }
        }
    }

    public void Left()
    {
        if (!isSwitchingLane)
        {
            SwitchLane(-1);
        }
    }

    public void Right()
    {
        if (!isSwitchingLane)
        {
            SwitchLane(1);
        }
    }

    public void CloseDoor()
    {
        if (npcleave.canDriveOff == true)
        {
            busdoorAnim.Play(animationName);
            StartCoroutine(MoveOut());
        }

        if (npcleave2.ticket3 == true)
        {
            busdoorAnim.Play(animationName);
            StartCoroutine(MoveOut());
        }
    }

    /// Bus Docking Code
    public void drive()
    {
        if (!worldStopped) // Check if the world is not stopped
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    public void buspark()
    {
        float step = finalspeed/2 * Time.deltaTime;
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetXPosition, step), transform.position.y, transform.position.z);
        currentLaneIndex = 0;
    }

    void GoBack()
    {
        float step = finalspeed/2 * Time.deltaTime;
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, gobackPosition, step), transform.position.y, transform.position.z);
    }

    ///Switching lane Code
    void SwitchLane(int direction)
    {
        currentLaneIndex = Mathf.Clamp(currentLaneIndex + direction, 0, lanes.Length - 1);
        StartCoroutine(MoveToLane(lanes[currentLaneIndex]));
    }

    void UpdateBusPosition()
    {
        StartCoroutine(MoveToLane(lanes[currentLaneIndex]));
    }

    IEnumerator MoveToLane(float targetX)
    {
        isSwitchingLane = true;

        while (Mathf.Abs(transform.position.x - targetX) > 0.01f)
        {
            float step = finalspeed/2 * Time.deltaTime;
            transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetX, step), transform.position.y, transform.position.z);
            yield return null;
        }

        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        isSwitchingLane = false;
    }

    IEnumerator MoveOut()
    {
        yield return new WaitForSeconds(5f);
        canPark = true;
        speed = 1;
        GoBack();

    }

    // Collision with stationary objects
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Traffic"))
        {
           
            // Stop the world for 2 seconds
            originalSpeed = speed;
            speed = 0;
            worldStopped = true;
            worldStopTimer = 2f;

            // Deactivate the stationary object
            other.gameObject.SetActive(false);
        }
    }
    void drive(float speed)
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}