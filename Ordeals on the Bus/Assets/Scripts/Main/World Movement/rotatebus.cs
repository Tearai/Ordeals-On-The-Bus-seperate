using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatebus : MonoBehaviour
{
    public GameObject StartPoint;
    public GameObject FinishedPoint;
    public float rotationSpeed = 5f; 
    public float rotationThreshold = 0f; 

    private bool isRotating = false;
    public bool canRotate;

    public GameObject map;

    public worldmove2 move2;
    public worldmove3 move3;

    public GameObject slowdown;

    public rotateFix fix;
    public GameObject trigger;
    public bool laneSwitch;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rotateworld();
            map.transform.parent = StartPoint.transform;
            move2.enabled = false;
            slowdown.SetActive(false);
            laneSwitch = true;
        }
    }

    void Rotateworld()
    {
        if (StartPoint != null && FinishedPoint != null && canRotate == false)
        {
            Quaternion targetRotation = FinishedPoint.transform.rotation;

            StartCoroutine(RotateToPoint(targetRotation));

            canRotate = true;
        }
    }

    IEnumerator RotateToPoint(Quaternion targetRotation)
    {
        while (Quaternion.Angle(StartPoint.transform.rotation, targetRotation) > rotationThreshold)
        {
            StartPoint.transform.rotation = Quaternion.RotateTowards(StartPoint.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Rotation reached destination!");
        map.transform.parent = null;
        isRotating = false;
        move3.enabled = true;
        trigger.SetActive(false);
        yield return new WaitForSeconds(2f);
        fix.go = true;


    }
}