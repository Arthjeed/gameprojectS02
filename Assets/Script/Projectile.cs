using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 dir;
    public float projectileSpeed = 5;
    public float power = 5;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyShipBehavior>().TakeDamage(power);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle") ||  collision.gameObject.CompareTag("Asteroide"))
            Destroy(gameObject);
    }
}
