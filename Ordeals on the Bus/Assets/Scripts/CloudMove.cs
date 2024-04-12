using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{


    private float speed;
    public float minSpeed;
    public float maxSpeed;
    public float minX;
    public float maxX;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x > maxX)
        {
            Vector3 newPos = new Vector3(minX, transform.position.y);
            transform.position = newPos;
        }
    }
}
