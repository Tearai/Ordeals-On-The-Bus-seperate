using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adtrigger : MonoBehaviour
{
    public GameObject[] ads;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.gameObject.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(EnableAdsRandomly());
        }
    }

    private IEnumerator EnableAdsRandomly()
    {
        ShuffleArray(ads);


        foreach (GameObject ad in ads)
        {
            ad.SetActive(true);
            yield return new WaitForSeconds(0.5f); 
        }
    }

    private void ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
