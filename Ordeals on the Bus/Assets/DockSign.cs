using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockSign : MonoBehaviour
{
    public GameObject Sign;
    public Animator SignAnim;

    public GameObject dockSFX;
    public GameObject notdockSFX;

    void Start()
    {
        SignAnim = Sign.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Docking"))
        {
            SignAnim.SetBool("isSign", true);
            dockSFX.SetActive(true);
            notdockSFX.SetActive(false);
        }

        if (other.CompareTag("Turn"))
        {
            SignAnim.SetBool("isTurn", true);
            dockSFX.SetActive(true);
            notdockSFX.SetActive(false);
        }

        if (other.CompareTag("Fast"))
        {
            SignAnim.SetBool("isOver", true);
            dockSFX.SetActive(true);
            notdockSFX.SetActive(false);
        }

        if (other.CompareTag("Stop"))
        {
            SignAnim.SetBool("isSign", false);
            dockSFX.SetActive(false);
            notdockSFX.SetActive(true);
        }

        if (other.CompareTag("Halt"))
        {
            SignAnim.SetBool("isTurn", false);
            SignAnim.SetBool("isOver", false);
            dockSFX.SetActive(false);
            notdockSFX.SetActive(true);

        }


    }
}