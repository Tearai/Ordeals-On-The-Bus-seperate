using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dilemma : MonoBehaviour
{
    public float speed = 5f; // Adjust this to change the speed of movement

    void Update()
    {
        // Move the object along the X-axis based on the speed
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
