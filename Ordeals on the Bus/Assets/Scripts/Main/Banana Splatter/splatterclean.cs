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

    public GameObject Wiper;
    public Animator wiperAnimator;
    public string wipeAnimation;
    public string wipeAnimation2;
    public bool playonce;


    public bool[] boolArray = new bool[5];

    public int colliderEnterCount = 0;

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
        if (boolArray[0] && cleaning == false)
        {
            StartCoroutine(FadeMaterialAlpha(0.8f, fadeDuration));
        }

        if (boolArray[1] && cleaning == false)
        {
            StartCoroutine(FadeMaterialAlpha(0.4f, fadeDuration));
        }

        if (boolArray[2] && cleaning == false)
        {
            StartCoroutine(FadeMaterialAlpha(0, fadeDuration));
            cleaning = true;
        }

        if (canShow == true)
        {
            SetMaterialAlpha(1f);
            cleaning = false;
        }

        if (lever.value == true)
        {
            //wiperAnimator.Play(wipeAnimation);
            playonce = true;
            if(playonce == true)
            {
                wiperAnimator.SetBool("isSwipe", true);
            }
        }

        if (lever.value == false && playonce == true)
        {
            //wiperAnimator.Play(wipeAnimation2);
            wiperAnimator.SetBool("isSwipe", false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Washer"))
        {
            colliderEnterCount++;

            // Activate the corresponding bool
            if (colliderEnterCount <= boolArray.Length)
            {
                boolArray[colliderEnterCount - 1] = true;
                StartCoroutine(DeactivateBoolAfterDelay(colliderEnterCount - 1, 1f)); // Change the duration as needed
            }
        }
    }

    IEnumerator DeactivateBoolAfterDelay(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        boolArray[index] = false;
    }
}