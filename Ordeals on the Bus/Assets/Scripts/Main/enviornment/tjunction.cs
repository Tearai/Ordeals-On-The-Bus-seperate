using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tjunction : MonoBehaviour
{
    public bool candisable;
    public worldmove world1;
    public worldmove2 world2;
    public GameObject right;
    public bool world3EnabledOnce = false;

    //Tuning parent
    public GameObject Parent;
    public GameObject mapp;

    //target location
    public GameObject worldmap;
    public Vector3 targetPosition = new Vector3(129.8f, 8.96f, -152.2f);
    public Quaternion targetRotation = Quaternion.Euler(Vector3.zero);
    public float movementSpeed = 0.1f;
    public float maxMovementSpeed = 0.5f;
    public float speedIncreaseRate = 0.1f;
    public float arrivalThreshold = 0.1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (candisable == true)
        {

            MoveToWorldTarget();
            mapp.transform.parent = Parent.transform;

        }

        if (HasArrivedAtPosition(worldmap.transform.position, targetPosition))
        {
            candisable = false;
            if (!world3EnabledOnce) // Check if world3 has been enabled once
            {
                print("test");
                world3EnabledOnce = true; // Set the flag to true
                mapp.transform.parent = null;
                world2.enabled = true;
                worldmap.transform.position = targetPosition;
                worldmap.transform.rotation = targetRotation;
            }

        }


    }

    void MoveToWorldTarget()
    {

        worldmap.transform.position = Vector3.Lerp(worldmap.transform.position, targetPosition, Time.deltaTime * movementSpeed);
        worldmap.transform.rotation = Quaternion.Lerp(worldmap.transform.rotation, targetRotation, Time.deltaTime * movementSpeed);

        movementSpeed = Mathf.Min(movementSpeed + speedIncreaseRate * Time.deltaTime, maxMovementSpeed);

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            candisable = true; ;
            world1.enabled = false;
            right.SetActive(false);



        }
    }


    bool HasArrivedAtPosition(Vector3 currentPosition, Vector3 targetPosition)
    {

        float distance = Vector3.Distance(currentPosition, targetPosition);

        return distance <= arrivalThreshold;
    }
}
