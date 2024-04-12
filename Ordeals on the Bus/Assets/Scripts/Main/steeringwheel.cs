using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steeringwheel : MonoBehaviour
{
    [Header("Wheel returning speed")]
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private float maxAngle = 90.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ReturnPosition();
        MaxTurn();
    }

    public void ReturnPosition()
    {
        if (transform.rotation.eulerAngles.x != 0)
        {
            // Define the target rotation (0 degrees on the X-axis)
            Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            // Interpolate between the current rotation and the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void MaxTurn()
    {
       
    }
}
