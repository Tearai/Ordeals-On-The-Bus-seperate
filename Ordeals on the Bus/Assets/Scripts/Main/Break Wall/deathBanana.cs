using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathBanana : MonoBehaviour
{
    public Animator NPC1Animations;
    public GameObject Animation;
    private Transform hipBone;
    public GameObject Bones;
    public Rigidbody[] _ragdollRigidbodies;

    public GameObject Dialogue;
    public GameObject Dialogue2;
    public bool canSpeak;


    // Start is called before the first frame update
    void Start()
    {
        NPC1Animations = Animation.GetComponent<Animator>();

        hipBone = NPC1Animations.GetBoneTransform(HumanBodyBones.Hips);
        _ragdollRigidbodies = Bones.GetComponentsInChildren<Rigidbody>();

        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpeak == true)
        {
            Dialogue.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Building"))
        {
            NPC1Animations.enabled = false;
            Dialogue.SetActive(false);
            canSpeak = false;
            Dialogue2.SetActive(true);

            foreach (var rigidbody in _ragdollRigidbodies)
            {
                rigidbody.isKinematic = false;
            }
        }
    }
}
