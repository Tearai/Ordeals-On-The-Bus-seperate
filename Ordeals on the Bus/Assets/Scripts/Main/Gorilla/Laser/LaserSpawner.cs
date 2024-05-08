using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    [Header("Laser")]
    public bool CanSpawnLaser;
    public GameObject Laser;
    public float ChargeTimer;
    public float TimerToShoot;
    public GameObject LaserVFX;
    public Animator LaserVFXAnimator;
    public bool CanShoot;
    public bool WaitTimeOver;
    public bool OneTime;
    public GameObject LaserButton;

    [Header("Cannon Animation")]
    public GameObject cannon;
    public Animator cannonanim;

    [Header("Cow")]
    public GameObject Cow;
    public Transform ProjectileLocation;
    public float launchForce;

    [Header("Monitor")]
    public GameObject laserarm;
    public Animator laserarmanim;

    [Header("VFX Timing")]
    public RedHollowControl vfx;
    public float VFXTime;
    public bool switchState;
    public GameObject shotSFX;

    // Start is called before the first frame update
    void Start()
    {
        LaserVFXAnimator = LaserVFX.GetComponent<Animator>();

        laserarmanim = laserarm.GetComponent<Animator>();

        cannonanim = cannon.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanShoot == true && WaitTimeOver == true && OneTime == false)
        {
            Shoot();
            OneTime = true;
        }

        if (OneTime == true && switchState == false)
        {
            switchState = true;
            vfx.Burst_Beam();
            StartCoroutine(LaserVFXTiming());
        }

        if (WaitTimeOver == true)
        {
            LaserButton.SetActive(true);
        }

        if(CanShoot)
        {
            shotSFX.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CanSpawnLaser = true;
            StartCoroutine(SpawnLaser());
            laserarmanim.SetBool("isShow", true);
        }
    }

    IEnumerator SpawnLaser()
    {
        yield return new WaitForSeconds(5f);
        cannonanim.SetBool("isShow", true);
        yield return new WaitForSeconds(ChargeTimer);
        LaserVFXAnimator.enabled = true;
        yield return new WaitForSeconds(TimerToShoot);
        WaitTimeOver = true;
        yield return new WaitForSeconds(30f);
        CanShoot = true;

    }

    IEnumerator LaserVFXTiming()
    {
        yield return new WaitForSeconds(VFXTime);
        vfx.Dead();
    }

    public void Shoot()
    {
        GameObject Bullet = Instantiate(Cow, ProjectileLocation.position, ProjectileLocation.rotation);

        Rigidbody rb = Bullet.GetComponent<Rigidbody>();

        rb.AddForce(ProjectileLocation.forward * launchForce, ForceMode.Impulse);
    }

    public void ButtonShoot()
    {
        CanShoot = true;
        
    }
}
