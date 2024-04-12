using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockSign : MonoBehaviour
{
    private Renderer rend;
    public float fadeSpeed = 2f; // Speed at which the alpha changes
    private bool fadingOut = false; // Flag to control fading out

    //new code
    public GameObject sign;

    void Start()
    {
        rend = GetComponent<Renderer>();
        // Set the initial alpha value
        Color color = rend.material.color;
        color.a = 0f;
        rend.material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Docking"))
        {
            // Set alpha to 100%
            //SetAlpha(1f);
            sign.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Docking"))
        {
            // Start fading out
            //fadingOut = true;
            sign.SetActive(false);
        }
    }

    void Update()
    {
        if (fadingOut)
        {
            // Fade out the alpha value
            FadeOut();
        }
    }

    void FadeOut()
    {
        Color color = rend.material.color;
        color.a -= fadeSpeed * Time.deltaTime;
        // Ensure alpha doesn't go below 0
        color.a = Mathf.Max(color.a, 0f);
        rend.material.color = color;

        // If alpha reaches 0, stop fading out
        if (color.a <= 0f)
        {
            fadingOut = false;
        }
    }

    void SetAlpha(float alpha)
    {
        Color color = rend.material.color;
        color.a = alpha;
        rend.material.color = color;
    }
}