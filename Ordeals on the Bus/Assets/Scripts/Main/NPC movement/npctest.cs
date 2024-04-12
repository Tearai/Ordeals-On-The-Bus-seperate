using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npctest : MonoBehaviour
{
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private List<Transform> seats = new List<Transform>(); // List of all available seats

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Find all seats in the scene and add them to the list
        FindSeats();

        // Make sure there are available seats
        if (seats.Count == 0)
        {
            Debug.LogError("No seats found in the scene!");
            enabled = false; // Disable this script if seats are not found
            return;
        }

        // Choose a random seat
        Transform randomSeat = seats[Random.Range(0, seats.Count)];

        // Move NPC to the selected seat position
        agent.SetDestination(randomSeat.position);
    }

    // Find all seats in the scene and add them to the list
    void FindSeats()
    {
        GameObject[] seatObjects = GameObject.FindGameObjectsWithTag("Seat"); // Assuming seats are tagged with "Seat"

        foreach (GameObject seatObject in seatObjects)
        {
            seats.Add(seatObject.transform);
        }
    }
}
