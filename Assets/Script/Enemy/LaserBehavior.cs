using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifeTime;

    private float clock = 0;
    private Transform dirSpawn;
    private Vector2 dir;
    
    void Start()
    {
        
    }

    void Update()
    {
        clock++;
        transform.position += new Vector3(dir.x * Time.deltaTime * speed, dir.y * Time.deltaTime * speed, 0);

        if (clock >= lifeTime)
            destroyLaser();
    }

    public void setValue(float _damage, float _speed)
    {
        damage = _damage;
        speed = _speed;
    }

    public void setDirection(Vector2 _dir)
    {
        dir = _dir;
    }

    public void destroyLaser()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            destroyLaser();
        }
        if (collision.collider.tag == "Shield")
        {
            destroyLaser();
        }
    }
}
