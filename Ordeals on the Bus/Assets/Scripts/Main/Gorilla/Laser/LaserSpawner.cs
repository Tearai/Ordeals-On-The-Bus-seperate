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

    [Header("Cow")]
    public GameObject Cow;
    public Transform ProjectileLocation;
    public float launchForce;

    [Header("VFX Timing")]
    public RedHollowControl vfx;
    public float VFXTime;
    public bool switchState;

    // Start is called before the first frame update
    void Start()
    {
        LaserVFXAnimator = LaserVFX.GetComponent<Animator>();
    }
        
    // Update is called once per frame
    void Update()
    {
        if (CanShoot == true && WaitTimeOver == true && OneTime == false)
        {
            Shoot();
            OneTime = true;
        }

        if(OneTime == true && switchState == false)
        {
            switchState = true;
            vfx.Burst_Beam();
            StartCoroutine(LaserVFXTiming());
        }

        if(WaitTimeOver == true)
        {
            LaserButton.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CanSpawnLaser = true;
            StartCoroutine(SpawnLaser());
        }
    }

    IEnumerator SpawnLaser()
    {
        Laser.SetActive(true);
        yield return new WaitForSeconds(ChargeTimer);
        LaserVFXAnimator.enabled = true;
        yield return new WaitForSeconds(TimerToShoot);
        WaitTimeOver = true;
        
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
