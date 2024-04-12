using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakBullet : MonoBehaviour
{
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
        // Check if the collider's GameObject has the "Obstacle" tag
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject); // Destroy the bullet GameObject
        }
    }
}
