using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testing : MonoBehaviour
{
    public GameObject NPC;
    public Transform Spawnlocation;

    public void Start()
    {
        StartCoroutine(SpawnNpc());
    }

    public void Update()
    {
        
    }


    IEnumerator SpawnNpc()
    {
        while (true)
        {
            Instantiate(NPC, Spawnlocation.position, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
}
