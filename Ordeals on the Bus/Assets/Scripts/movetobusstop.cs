using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetobusstop : MonoBehaviour
{
    public GameObject bus;
    public GameObject busstop;
    public float speed = 5;
    //private bool isMoving = false;

    void Update()
    {
        // Calculate the direction from the bus to the bus stop.
        Vector3 direction = busstop.transform.position - bus.transform.position;

        // Normalize the direction vector to get a unit vector.
        direction.Normalize();

        // Start moving the bus.
        StartCoroutine(MoveBus(direction));
    }

    IEnumerator MoveBus(Vector3 direction)
    {
        //isMoving = true;

        // Calculate the distance between the bus and the bus stop.
        float distance = Vector3.Distance(bus.transform.position, busstop.transform.position);

        while (distance > 5f)
        {
            // Move the bus towards the bus stop.
            bus.transform.position = Vector3.MoveTowards(bus.transform.position, busstop.transform.position, speed * Time.deltaTime);

            // Update the distance.
            distance = Vector3.Distance(bus.transform.position, busstop.transform.position);

            yield return null;
        }   

        //isMoving = false;
    }
}
