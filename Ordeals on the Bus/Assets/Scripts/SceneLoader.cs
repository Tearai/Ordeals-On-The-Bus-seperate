using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public worldmove worldmove; // Reference to the desired worldmove script
    public Breakbusback Eatfnc;

    public void Start()
    {
        if (worldmove != null)
        {
            // Call the method on the referenced script
            worldmove.drive();
        }
        else
        {
           // Debug.LogError("worldmove script reference not set in the Inspector.");
        }
        if (Eatfnc != null)
        {
            // Call the method on the referenced script
            Eatfnc.EnableEat();
        }
    }
    public void Tracker()
    {
        
    }

    private bool isCoroutineRunning = false;

   public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCoroutineRunning)
        {
            Debug.Log("NPC entered the trigger");

            // Reference the Breakbusback script and call EnableEat method
            if (Eatfnc != null)
            {
                Eatfnc.EnableEat();
            }
            Debug.Log("Player entered the trigger");

            // Find the GameObject with the worldmove script using a tag
            GameObject worldmoveObject = GameObject.FindWithTag("World");
             

            if (worldmoveObject != null)
            {
                // Get the worldmove component from the found GameObject
                worldmove playerWorldMove = worldmoveObject.GetComponent<worldmove>();

                if (playerWorldMove != null)
                {
                    Debug.Log("worldmove component found");

                    // Save the initial speed
                    float initialSpeed = playerWorldMove.speed;

                    // Change the speed of the referenced worldmove script attached to the "Player" object.
                    playerWorldMove.speed = 0.01f;

                    // Start the coroutine only if it's not already running
                    if (!isCoroutineRunning)
                    {
                        StartCoroutine(ResetSpeedAfterDelay(playerWorldMove, initialSpeed));
                    }

                    // Destroy the GameObject (assuming this script is attached to a GameObject).
                   

                    Debug.Log("GameObject destroyed");

                }
                else
                {
                    Debug.LogError("worldmove component not found on the GameObject with the tag 'World'.");
                }
            }
            else
            {
                Debug.LogError("GameObject with the tag 'World' not found.");
            }
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
       

    }

    // Coroutine to reset speed after a delay
    IEnumerator ResetSpeedAfterDelay(worldmove playerWorldMove, float initialSpeed)
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(3f);

        // Reset the speed to the initial value
        playerWorldMove.speed = 5f;

        isCoroutineRunning = false;
        Destroy(this.gameObject);
    }
    
}