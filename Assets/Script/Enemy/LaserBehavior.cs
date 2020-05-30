using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifeTime;

    private float clock = 0;
    
    void Start()
    {

    }

    void Update()
    {
        clock++;
        transform.position += transform.forward * speed;
        if (clock >= lifeTime)
            destroyLaser();
    }

    public void setValue(float _damage, float _speed)
    {
        damage = _damage;
        speed = _speed;
    }

    public void setDirection(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void destroyLaser()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            print("ouill");
            destroyLaser();
        }
        if (collision.collider.tag == "Shield")
        {
            destroyLaser();
        }
    }
}
