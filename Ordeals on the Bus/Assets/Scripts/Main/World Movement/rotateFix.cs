using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateFix : MonoBehaviour
{
    public GameObject map;

    public bool go = false; // Variable to control rotation

    public float value;

    public float tolerance = 0.01f;
    // Update is called once per frame
    void Update()
    {
        if (go)
        {
            // Increase the rotation around the Y axis
            map.transform.Rotate(0f, value, 0f); // Adjust rotation increment as needed

            // Check if the Y rotation is close to 179.9 degrees
            if (Mathf.Abs(map.transform.rotation.eulerAngles.y - 179.99f) < tolerance)
            {
                go = false; // Set go to false
                map.transform.rotation = Quaternion.Euler(map.transform.rotation.eulerAngles.x, 180f, map.transform.rotation.eulerAngles.z);
            }
        }
    }
}
