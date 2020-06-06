using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 dir;
    public float projectileSpeed = 5;
    public float projectileDamage = 5;
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        // transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        transform.position += new Vector3 (dir.x * Time.deltaTime * projectileSpeed, dir.y * Time.deltaTime * projectileSpeed, 0);
    }

    public void SetDirection(Vector2 direction)
    {
        dir = direction;
        dir.Normalize();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.tag);
        if (collision.collider.tag == "Obstacle" || collision.collider.tag == "Enemy")
            Destroy(gameObject);

    }
}
