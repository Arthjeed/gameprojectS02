﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipMovement : MonoBehaviour
{
    public float maxSpeed = 20;
    public float speedMissile = 0.2f;
    public int reloadTime = 20;
    public int masse = 100;
    public Transform reactorLight;
    public GameObject laser;
    public Transform laserSpawn;

    private static readonly int TIMEREACTOR = 50;

    private float reactorState = TIMEREACTOR;
    private Vector3 reactorDown = new Vector3(0.2f, 0.2f, 1f);
    private Vector3 reactorUp = new Vector3(1, 1, 1);
    private GameObject player;

    private int reloadState = 0;
    private int count = 0;
    private float damage;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Spaceship");
    }

    void Update()
    {

    }

    public void setDamage(float _damage)
    {
        damage = _damage;
    }

    public void goForward(float speed)
    {
        reactorIgnit();
        transform.localPosition += transform.forward * Time.deltaTime * (maxSpeed * speed);
    }

    public void stop()
    {
        rb.velocity = rb.velocity + new Vector2(-rb.velocity.x /100, -rb.velocity.y /100);
        reactorShutDown();
    }

    public void reactorIgnit()
    {
        if (reactorState < TIMEREACTOR)
        {
            reactorState++;
            Vector3 newscale = new Vector3(((reactorState / TIMEREACTOR) * reactorUp.x), (reactorState / TIMEREACTOR) * reactorUp.y, 0);
            reactorLight.localScale = reactorDown + newscale;
        }
    }

    public void reactorShutDown()
    {
        if (reactorState > 0)
        {
            reactorState = reactorState -1;
            Vector3 newscale = new Vector3(((reactorState / TIMEREACTOR) * reactorUp.x), (reactorState / TIMEREACTOR) * reactorUp.y, 0);
            reactorLight.localScale = reactorDown + newscale;
        }
    }

    public void strafe(int direction, Transform player)
    {
        transform.RotateAround(player.position, new Vector3 (0, 0, direction), (maxSpeed / 2) * Time.deltaTime); ;
    }

    public void turn(Vector3 direction)
    {
        if (direction == transform.right)
        {
/*            if (transform.localEulerAngles.z < 45)
                transform.localEulerAngles += new Vector3(0, 0, 1) * Time.deltaTime * 90;*/
            rb.rotation += 0.5f;
        }
        if (direction == -transform.right)
        {
            rb.rotation += -0.5f;
/*            if (transform.localEulerAngles.z > 315 || (transform.localEulerAngles.z > -45 && transform.localEulerAngles.z < 45))
                transform.localEulerAngles += new Vector3(0, 1, -1) * Time.deltaTime * 90;
            else
                transform.localEulerAngles += new Vector3(0, 1, 0) * Time.deltaTime * 90;*/
        }
}

    public void stabilize()
    {
        if (transform.localEulerAngles.z > 1 && transform.localEulerAngles.z < 180)
            transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, 0, -1) * Time.deltaTime * 90;
        if (transform.localEulerAngles.z < 360 && transform.localEulerAngles.z > 180)
            transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, 0, 1) * Time.deltaTime * 90;

    }
    public void shoot()
    {
        reloadState++;
        if (reloadState == reloadTime)
        {
            reloadState = 0;
            Quaternion rotation;
            Vector2 dir = new Vector2(transform.forward.x, transform.forward.y);

            if (player.transform.localPosition.x < transform.localPosition.x)
                rotation = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.x + 270));
            else
                rotation = Quaternion.Euler(new Vector3(0, 0, 360 - transform.eulerAngles.x + 90));

            GameObject newProj = PhotonNetwork.Instantiate("ProjectileEnemy", laserSpawn.position, rotation);
            newProj.GetComponent<LaserBehavior>().setDirection(dir);
            newProj.GetComponent<LaserBehavior>().setValue(damage, speedMissile);

        }
    }
}
