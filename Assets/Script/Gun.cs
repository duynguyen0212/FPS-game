using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem shootingEffect;
    public GameObject impactEffect;
    public float fireRate = 15f;
    public float nextTimeToFire = 0f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 2f;

    private bool reloading;
    public Animator animator;
    public AudioSource shootingSound;
    public AudioSource reloadingSound;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        shootingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPause){

            if(reloading){
                return;
            }
            if(currentAmmo <=0){
                StartCoroutine(Reload());
                return;
            }

            if(Input.GetButton("Fire") && Time.time >= nextTimeToFire){
                nextTimeToFire = Time.time +1f/fireRate;
                Shoot();
                
            }

            if(Input.GetKeyDown(KeyCode.R)){
                StartCoroutine(Reload());
                return;
            }
        }

    }

    IEnumerator Reload(){
        reloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        reloadingSound.Play();
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        reloading = false;
    }


    void Shoot(){
        shootingEffect.Play();
        shootingSound.Play();
        currentAmmo--;
        
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            //Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(hit.collider.CompareTag("Enemy")){
                enemy.TakeDamage(damage);
            }
            
            GameObject effect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(effect, 1f);

        }

    }
}
