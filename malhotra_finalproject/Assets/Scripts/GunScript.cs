using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float reloadTime = 1f;
    public int maxAmmo = 10;

    public Text ammoDisplay;
    public Text scoreDisplay;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Text reloadText;
    public Animator animator;
    public LayerMask mask;
    public AudioSource gunFire;

    private float nextTimeToFire = 0f;
    private int currentAmmo;
    private bool isReloading = false;
    // Update is called once per frame

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    void Update()
    {
        //displays ammo and score
        ammoDisplay.text = "Ammo: " + currentAmmo.ToString();
        scoreDisplay.text = "Score: " + Target.score.ToString();
        if (isReloading)
            return;

        //if player is out of ammor or presses r key, reload gun
        if (currentAmmo <= 0  || (Input.GetKey(KeyCode.R) && currentAmmo != maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }
        //if the thompson has 8 bullets or less or the pistol has 4 bullets or less, display low ammo message
        if((currentAmmo <= 8 && gameObject.tag != "Sidearm") || (currentAmmo <= 4 && gameObject.tag == "Sidearm"))
        {
            reloadText.enabled = true;
        }
        else 
        {
            reloadText.enabled = false;
        }
        //if the gun is not a sidearm
        if (gameObject.tag != "Sidearm")
        {
            //if player presses/holds the left mouse, auto fire
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        //if the gun is a sidearm, only allow single shot firing
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    //reload function (Enumerator function) tells the animator to switch to true for reloading and play reload animation, then fill ammo and stop animation
    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunFire.Play();

        currentAmmo--;

        RaycastHit hit;
        //creates the ray and checks if it hits something
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //checks if the ray is hitting a target and takes damage from it
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1f);
        }
    }
}
