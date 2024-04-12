using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSmokeLeaver : MonoBehaviour
{
    public GameObject SmokerCleaner;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
   public void Wiper()
    {
        SmokerCleaner.SetActive(true);
    }
}
