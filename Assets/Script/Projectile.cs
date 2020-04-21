using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 dir;
    public float projectileSpeed = 5;
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.position += new Vector3 (dir.x * Time.deltaTime * projectileSpeed, dir.y * Time.deltaTime * projectileSpeed, 0);
    }

    public void SetDirection(Vector2 direction)
    {
        dir = direction;
    }
}
