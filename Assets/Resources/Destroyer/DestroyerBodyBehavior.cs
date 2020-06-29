using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyerBodyBehavior : MonoBehaviour
{
    public GameObject crystal;
    public GameObject lightCrystal;
    
    private GameObject target;

    private float _damage = 1.0f;
    private float _speedMissile = 150.0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Spaceship");
    }

    public void shoot(Vector3 targetPos)
    {
        Vector2 dir = targetPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, 0));// transform.rotation;// * parent.transform.rotation;
        GameObject newProj = PhotonNetwork.Instantiate("ProjectileBoss", transform.position, Quaternion.identity);
        newProj.GetComponent<BossProjectile>().SetDirection(dir);
        newProj.GetComponent<BossProjectile>().setValue(_damage, _speedMissile);
        //newProj.GetComponent<LaserBehavior>().setValue(_damage, _speedMissile * Time.deltaTime);
    }

    public bool isAlive()
    {
        return crystal.activeSelf;
    }

   void OnTriggerEnter2D(Collider2D col)
   {
        if (col.CompareTag("AllyProjectile") && crystal.activeSelf) {
            crystal.SetActive(false);
            lightCrystal.SetActive(false);
            PhotonNetwork.Destroy(col.gameObject);
       }
   }
}
