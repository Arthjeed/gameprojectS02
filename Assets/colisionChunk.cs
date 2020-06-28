using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisionChunk : MonoBehaviour
{
    public GameObject crystal;
    public GameObject lightCrystal;
    
    public GameObject target;
    public GameObject projectile;

   /* void shoot()
    {
        Vector2 dir = transform.position - target.transform.positon;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, 0));// transform.rotation;// * parent.transform.rotation;
        GameObject newProj = Instantiate("ProjectileCanon", dir, rotation);
        //newProj.GetComponent<Projectile>().SetDirection(dir);
    }*/


   void OnTriggerEnter2D(Collider2D col)
   {
        if (col.CompareTag("AllyProjectile")) {
            crystal.SetActive(false);
            lightCrystal.SetActive(false);
       }
   }
}
