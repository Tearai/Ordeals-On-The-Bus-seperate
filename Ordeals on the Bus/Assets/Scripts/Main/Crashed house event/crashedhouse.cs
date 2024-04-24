using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crashedhouse : MonoBehaviour
{
    public worldmove map;
    public float driveBackTime;

    public GameObject turnObject;

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
            StartCoroutine(crashedEvent()); 
        }
    }

    IEnumerator crashedEvent()
    {
        map.driveforward = true;
        map.speed = 1;
        yield return new WaitForSeconds(driveBackTime);
        map.driveforward = false;
        turnObject.SetActive(true);
    }
}
