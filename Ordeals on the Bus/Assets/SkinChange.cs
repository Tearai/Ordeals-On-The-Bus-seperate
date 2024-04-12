using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChange : MonoBehaviour
{
    
    public Material Skin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bullet"))
        {
            // Change the color of the object to yellow
            
            Skin.color = Color.yellow;
            
        }
    }
}
