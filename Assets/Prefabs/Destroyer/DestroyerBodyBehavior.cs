using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyerBodyBehavior : MonoBehaviour
{
    public GameObject crystal;
    public GameObject lightCrystal;
    
    private GameObject target;
    public GameObject projectile;

    private float _damage = 1;
    private float _speedMissile = 10;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Spaceship");
    }

    public void shoot()
    {
        Vector2 dir = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector2(dir.x, dir.y));// transform.rotation;// * parent.transform.rotation;
        GameObject newProj = PhotonNetwork.Instantiate("ProjectileTurret", dir, rotation);
        newProj.GetComponent<Projectile>().SetDirection(dir);
        //newProj.GetComponent<LaserBehavior>().setValue(_damage, _speedMissile * Time.deltaTime);
    }

   void OnTriggerEnter2D(Collider2D col)
   {
        if (col.CompareTag("AllyProjectile")) {
            crystal.SetActive(false);
            lightCrystal.SetActive(false);
       }
   }
}
