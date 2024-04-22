using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windowdown : MonoBehaviour
{
    public GameObject window;
    public GameObject uppos;
    public GameObject downpos;
    public bool toggle;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(toggle == true)
        {
            window.transform.position = Vector3.MoveTowards(window.transform.position, downpos.transform.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            window.transform.position = Vector3.MoveTowards(window.transform.position, uppos.transform.position, Time.deltaTime * moveSpeed);
        }
    }
}
