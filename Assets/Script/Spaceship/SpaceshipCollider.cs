using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollider : MonoBehaviour
{
    private GameObject parent;
    private SpaceShip ship;

    void Start()
    {
        parent = transform.parent.gameObject;
        ship = parent.GetComponent<SpaceShip>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("SpaceshipCollider");
        print(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Obstacle"))
            ship.Crash();
    }
}
