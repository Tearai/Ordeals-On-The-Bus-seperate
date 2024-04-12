using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsMovement2 : MonoBehaviour
{
    public float speed;
    public float movingSpeed;
    public bool lane1;
    public bool lane2;

    public float _crashedState;

    public float _durationOfCrash = 1.0f;

    private bool isCrashing = false;

    Gorilla gorilla;
    GorillaSpawn canSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gorilla = GameObject.FindGameObjectWithTag("Boss").GetComponent<Gorilla>();
        canSpeed = GameObject.FindGameObjectWithTag("GorillaSpawn").GetComponent<GorillaSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpeed.move == true)
        {
            speed = movingSpeed;
        }

        if (lane1 == true)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (lane2 == true)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            speed = 0;
            if (!isCrashing)
            {
                gorilla.Grab = false;
                isCrashing = true;
                StartCoroutine(CrashAnimation());
            }
        }
    }

    IEnumerator CrashAnimation()
    {
        float timer = 0f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(transform.localScale.x, _crashedState, transform.localScale.z);

        while (timer < _durationOfCrash)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / _durationOfCrash);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // Ensure we reach the exact scale at the end
    }
}
