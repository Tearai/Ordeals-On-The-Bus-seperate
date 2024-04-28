using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class narratordialoguesystem : MonoBehaviour
{
    [Header("Dialogue Lists")]
    public List<GameObject> NarratorDialogues = new List<GameObject>();
    public int currentDialogueIndex = 0;

    [Header("Dialogue Lists")]
    public List<GameObject> NarratorDialogues2 = new List<GameObject>();
    public int currentDialogueIndex2 = 0;
    public bool Section2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dialogue"))
        {
            if (Section2 == false)
            {
                StartCoroutine(nextDialogue());
            }

            if (Section2 == true)
            {
                StartCoroutine(nextDialogue2());
            }

        }
    }

    IEnumerator nextDialogue()
    {
        NarratorDialogues[currentDialogueIndex].SetActive(false);
        yield return new WaitForSeconds(1f);
        currentDialogueIndex++;
        yield return new WaitForSeconds(1f);
        NarratorDialogues[currentDialogueIndex].SetActive(true);

    }

    IEnumerator nextDialogue2()
    {
        NarratorDialogues2[currentDialogueIndex2].SetActive(false);
        yield return new WaitForSeconds(1f);
        currentDialogueIndex2++;
        yield return new WaitForSeconds(1f);
        NarratorDialogues2[currentDialogueIndex2].SetActive(true);

    }
}
