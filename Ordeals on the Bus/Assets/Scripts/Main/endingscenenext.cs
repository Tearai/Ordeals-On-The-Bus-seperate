using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endingscenenext : MonoBehaviour
{

    public GameObject eyefade;
    public Animator eyefadeanim;
    public string Level;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void endlevel()
    {
        StartCoroutine(nextscene());
    }

    IEnumerator nextscene()
    {
        eyefadeanim.SetBool("isEnd", true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(Level);
    }
}
