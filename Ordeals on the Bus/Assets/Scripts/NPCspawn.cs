using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCspawn : MonoBehaviour
{
    public GameObject[] npcPrefabs; // Array of regular NPC prefabs, assign them in the Unity Editor
    public GameObject specialNPCPrefab; // Assign the special NPC prefab in the Unity Editor
    public int numberOfRegularNPCs = 4; // Adjust the number of regular NPCs you want to instantiate
    public float spaceBetweenNPCs = 2.0f; // Adjust the space between NPCs

    public List<GameObject> spawnedNPCs = new List<GameObject>(); // Keep track of spawned NPCs
    private int currentNPCIndex = 0; // Keep track of the current NPC index

    void Start()
    {
        InstantiateNPCs();
    }

    void Update()
    {
        // Check for the 'L' key press
        if (Input.GetKeyDown(KeyCode.L))
        {
            ActivateNextBoardingScript();
        }
    }

    void InstantiateNPCs()
    {
        // Spawn regular NPCs
        for (int i = 0; i < numberOfRegularNPCs; i++)
        {
            GameObject selectedPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
            GameObject npc = Instantiate(selectedPrefab, transform.position - new Vector3(0, 0, i * spaceBetweenNPCs), Quaternion.identity);
            npc.transform.parent = transform;
            spawnedNPCs.Add(npc);
            npc.GetComponent<npcmovement>().enabled = false;
            npc.GetComponent<NavMeshAgent>().enabled = false;
        }

        // Spawn special NPC at the end
        GameObject specialNPC = Instantiate(specialNPCPrefab, transform.position - new Vector3(0, 0, numberOfRegularNPCs * spaceBetweenNPCs), Quaternion.identity);
        specialNPC.transform.parent = transform;
        spawnedNPCs.Add(specialNPC);
        specialNPC.GetComponent<NavMeshAgent>().enabled = false;
        specialNPC.GetComponent<vipmovement>().enabled = false;
    }

    void ActivateNextBoardingScript()
    {
        // Check if there are more NPCs to activate
        if (currentNPCIndex < spawnedNPCs.Count)
        {
            if(spawnedNPCs[currentNPCIndex].CompareTag("SpecialNPC"))
            {
                spawnedNPCs[currentNPCIndex].GetComponent<vipmovement>().enabled = true;
                spawnedNPCs[currentNPCIndex].GetComponent<NavMeshAgent>().enabled = true;
            }
            else
            {
                spawnedNPCs[currentNPCIndex].GetComponent<npcmovement>().enabled = true;
                spawnedNPCs[currentNPCIndex].GetComponent<NavMeshAgent>().enabled = true;
            }
            // Enable the BoardingBus script for the current NPC
            
            


            // Increment the index for the next NPC
            currentNPCIndex++;
        }
        else
        {
            // All NPCs have boarded the bus
            Debug.Log("All NPCs have boarded the bus!");
        }
    }
}