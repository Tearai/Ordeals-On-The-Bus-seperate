using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;


public class splatterclean : MonoBehaviour
{
    public Material splat;
    public bool canShow;
    public bool cleaned1;
    public bool cleaned2;
    public bool cleaned3;
    public bool cleaning;
    public float fadeDuration = 1f;

    public XRLever lever;
    public bool leverTrue;
    public bool leverSecond;
    public bool leverThird;
    public bool leverFourth;
    public bool leverFifth;
    public bool leverSixth;
    public bool leverStart;

    public GameObject Wiper;
    public Animator wiperAnimator;
    public string wipeAnimation;
    public bool playonce;

    // Start is called before the first frame update
    void Start()
    {
        wiperAnimator = Wiper.GetComponent<Animator>();
        cleaning = true;
    }

    IEnumerator FadeMaterialAlpha(float targetAlpha, float duration)
    {
        Color startColor = splat.color;
        Color targetColor = startColor;
        targetColor.a = targetAlpha;

        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            splat.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        splat.color = targetColor;
        cleaned1 = false;
        cleaned2 = false;
        cleaned3 = false;
    }

    void SetMaterialAlpha(float alpha)
    {
        if (splat != null)
        {
            Color color = splat.color;
            color.a = alpha;
            splat.color = color;
        }
        else
        {
            Debug.LogError("Material not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cleaned1)
        {
            StartCoroutine(FadeMaterialAlpha(0.8f, fadeDuration));
        }

        if (cleaned2)
        {
            StartCoroutine(FadeMaterialAlpha(0.4f, fadeDuration));
        }

        if (cleaned3)
        {
            StartCoroutine(FadeMaterialAlpha(0, fadeDuration));
        }

        if (canShow == true)
        {
            SetMaterialAlpha(1f);
            cleaning = false;
        }

        // Check if lever is activated and set cleaned variables accordingly
        if (leverTrue == true)
        {
            cleaned1 = true;
        }

        if (leverThird == true)
        {
            cleaned2 = true;
        }

        if(leverFifth == true)
        {
            cleaned3 = true;
        }



        if(lever.value == true && leverStart == false && cleaning == false)
        {
            leverTrue = true;
            leverStart = true;
        }

        if(lever.value == false && leverTrue == true)
        {
            leverSecond = true;
            leverTrue = false;
        }

        if (lever.value == true && leverSecond == true)
        {
            leverThird = true;
            leverSecond = false;
        }

        if (lever.value == false && leverThird == true)
        {
            leverFourth = true;
            leverThird = false;
        }

        if (lever.value == true && leverFourth == true)
        {
            leverFifth = true;
            leverFourth = false;
        }

        if (lever.value == false  && leverFifth == true)
        {
            leverSixth = true;
            leverFifth = false;
        }

        if(lever.value == true && playonce == false)
        {
            wiperAnimator.Play(wipeAnimation);
            playonce = true;
        }

        if(lever.value == false)
        {
            playonce = false;
        }
    }
}