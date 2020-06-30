using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BossProjectile : MonoBehaviour
{
    private Vector2 dir;
    public float projectileSpeed;
    //public float power = 5;
    private AudioSource audioData;
    public float damage;

    private PhotonView PV;

    void Start()
    {
        PV = transform.GetComponent<PhotonView>();
        audioData = GetComponent<AudioSource>();
        audioData.PlayOneShot(audioData.clip, 1);
        Destroy(gameObject, 5f);
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

    public void setValue(float _damage, float _speed)
    {
        damage = _damage;
        projectileSpeed = _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy")) || collision.gameObject.CompareTag("Shield") || collision.gameObject.CompareTag("Obstacle")) {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
