using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollider : MonoBehaviour
{
    private GameObject parent;
    private SpaceShip ship;
    private UraniumPickUp uranium;
    private HealthManager health;
    private DropBehavior drop;

    void Start()
    {
        parent = transform.parent.gameObject;
        ship = parent.GetComponent<SpaceShip>();
        uranium = GetComponent<UraniumPickUp>();
        health = GetComponent<HealthManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
            ship.Crash();
        if (collision.gameObject.CompareTag("PickUp"))
        {
            drop = collision.gameObject.GetComponent<DropBehavior>();
            if (drop.type == DropBehavior.TypeDrop.Health)
                health.changeHealth(collision.gameObject.GetComponent<DropBehavior>().value * 100);
            if (drop.type == DropBehavior.TypeDrop.Uranium)
                uranium.addUranium(collision.gameObject.GetComponent<DropBehavior>().value);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            health.changeHealth(- collision.gameObject.GetComponent<LaserBehavior>().damage);
            collision.gameObject.GetComponent<LaserBehavior>().explose();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyShipBehavior>().DestroyShip();
            health.changeHealth(-25);
        }

    }
}
