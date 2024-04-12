using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class worldmove3 : MonoBehaviour
{

    // Variables for world movement
    public float speed = 1;
    public bool candrive;
    public bool canspeed;

    // Variables for parking
    public float targetXPosition = 114.32f; // Change back to X position
    public float moveSpeed = 1f;
    public bool canPark;

    //Speed
    public float finalspeed;


    // Variables for going forward
    public float gobackPosition = 109.42f;

    // Variables for switching lanes
    public float[] lanes;
    private int currentLaneIndex = 1;
    public bool isSwitchingLane = false;

    // Variables for bus leaving
    public NPC4 npcleave3;


    public GameObject busdoor;
    public Animator busdoorAnim;
    public string animationName;

    // Collision with stationary objects
    bool worldStopped = false;
    float originalSpeed;
    float worldStopTimer = 0f;

    //slider
    public XRSlider slider;
    // Variables for world movement
    float minSpeed = 1f; // Minimum speed
    float maxSpeed = 5f; // Maximum speed
    public float newSpeed;
    public float sliderValue;

    public XRKnob wheel;
    public float wheelangle;


    void Start()
    {
        busdoorAnim = busdoor.GetComponent<Animator>();
        canspeed = true;
        //UpdateBusPosition();
    }

    void Update()
    {
        //Wheel value
        wheelangle = wheel.value;

        if (wheelangle == 1)
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

        finalspeed = speed * newSpeed;

        drive(finalspeed); // Drive with the new speed



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
        if (npcleave3.canDriveOff == true)
        {
            busdoorAnim.Play(animationName);
            StartCoroutine(MoveOut());
        }
    }

    /// Bus Docking Code

    void drive(float speed)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void drive()
    {
        if (!worldStopped) // Check if the world is not stopped
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // Change back to X axis
        }
    }

    public void buspark()
    {
        float step = finalspeed/2 * Time.deltaTime;
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetXPosition, step), transform.position.y, transform.position.z); // Change back to X axis
        currentLaneIndex = 0;
    }

    void GoBack()
    {
        float step = finalspeed/2 * Time.deltaTime;
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, gobackPosition, step), transform.position.y, transform.position.z); // Change back to X axis
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

        while (Mathf.Abs(transform.position.x - targetX) > 0.01f) // Change back to X axis
        {
            float step = finalspeed/2 * Time.deltaTime;
            transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetX, step), transform.position.y, transform.position.z); // Change back to X axis
            yield return null;
        }

        transform.position = new Vector3(targetX, transform.position.y, transform.position.z); // Change back to X axis
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
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Traffic"))
        {
            Debug.Log("Collision detected with stationary object");

            // Stop the world for 2 seconds
            originalSpeed = speed;
            speed = 0;
            worldStopped = true;
            worldStopTimer = 2f;

            // Deactivate the stationary object
            other.gameObject.SetActive(false);
        }
    }


}