using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatelaserdialogue : MonoBehaviour
{
    public GameObject Dialogue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void activateDialogue()
    {
        Dialogue.SetActive(true);
    }
}
