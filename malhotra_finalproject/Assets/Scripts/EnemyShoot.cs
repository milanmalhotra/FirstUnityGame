using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public float fireRate = 15f;
    public POV pov;
    public ParticleSystem muzzleFlash;
    public AudioSource gunShot;

    private float nextTimeToFire = 1f;

    // Update is called once per frame
    void Update()
    {
        if (pov.isAggro && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }
    
    void Shoot()
    {
        muzzleFlash.Play();
        gunShot.Play();

        RaycastHit hit;
        //creates the ray and checks if it hits something
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            //print(hit.transform.gameObject.name);
            //checks if the ray is hitting a target and takes damage from it
            PlayerHealth ph = hit.transform.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
            }
        }
    }
}
