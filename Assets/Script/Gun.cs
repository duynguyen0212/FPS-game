using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem shootingEffect;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire")){
            Shoot();
        }
    }

    void Shoot(){
        shootingEffect.Play();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            //Debug.Log(hit.transform.name);
            

            GameObject effect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(effect, 1f);
        }

    }
}
