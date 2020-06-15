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

    private void OnTriggerEnter2D(Collider2D collision)
{
    print("turret col");
        if (collision.tag == "Player" || collision.tag == "AllyProjectile")
            turret.GetComponent<TurretEnemyBehavior>().DestroyShip();
    }
}
