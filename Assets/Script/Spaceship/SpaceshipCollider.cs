using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollider : MonoBehaviour
{
    private GameObject parent;
    private SpaceShip ship;
    private UraniumPickUp uranium;

    void Start()
    {
        parent = transform.parent.gameObject;
        ship = parent.GetComponent<SpaceShip>();
        uranium = parent.GetComponent<UraniumPickUp>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
            ship.Crash();
        if (collision.gameObject.CompareTag("PickUp"))
        {
            uranium.addUranium(collision.gameObject.GetComponent<DropBehavior>().value);
            Destroy(collision.gameObject);
        }
    }
}
