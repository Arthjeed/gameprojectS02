using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCollision : MonoBehaviour
{
    public GameObject turret;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "AllyProjectile")
            turret.GetComponent<TurretEnemyBehavior>().DestroyShip();
    }
}
