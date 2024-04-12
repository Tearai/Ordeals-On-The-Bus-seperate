using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonkey : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Traffic;
    public ButtonCannon But;
    public CountdownTimer HAHA;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monkey"))
        {
            Traffic.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(true);
            HAHA.StartCountdown();
            But.SpawnCannon();
        }
    }
}
