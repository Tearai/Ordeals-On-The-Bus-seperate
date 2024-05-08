using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stampnextlevel : MonoBehaviour
{
    public GameObject eyefade;
    public Animator eyefadeanim;
    public string Level;
    public GameObject Dialogue;
    public GameObject notstamped;
    public GameObject stamped;

    // Start is called before the first frame update
    void Start()
    {
        eyefadeanim = eyefade.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            
            StartCoroutine(nextscene());
        }
    }

    IEnumerator nextscene()
    {
        notstamped.SetActive(false);
        stamped.SetActive(true);
        Dialogue.SetActive(true);
        yield return new WaitForSeconds(9f);
        eyefadeanim.SetBool("isFade", true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(Level);
    }
}
