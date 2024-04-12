using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MafiaBananas : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Shooting");
            StartCoroutine(DelayDeath());
        }
    }

    // Update is called once per frame
    IEnumerator DelayDeath()
    {
       
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(2);
    }
}
