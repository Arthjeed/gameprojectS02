using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    private AudioSource audioData;
    public float damage;
    public float speed;
    public float lifeTime;
    public ParticleSystem Explosion;
    public GameObject laser;

    private float clock = 0;
    private Transform dirSpawn;
    private Vector2 dir;
    private bool isExploding = false;
    
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isExploding)
        {
            clock++;
            transform.position += new Vector3(dir.x * Time.deltaTime * speed, dir.y * Time.deltaTime * speed, 0);
            audioData.Play();
        } else if (Explosion.time > 1)
        {
            destroyLaser();
        }

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
        if ((collision.collider.tag == "Player" || collision.collider.tag == "Shield" || collision.collider.tag == "Obstacle") && !isExploding)
        {
            isExploding = true;
            Explosion.Stop();
            Explosion.time = 0;
            Explosion.Play();
            laser.SetActive(false);
        }
    }
}
