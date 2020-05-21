using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniShip : MonoBehaviour
{
    public float maxSpeed = 20;
    public float chargeSpeed = 10f;
    public float crashSlowSpeed = 0.4f;
    private float speed = 0;
    public Transform parent;
    public GameObject projectile;
    private Vector3 currentVelo;
    private Vector3 crashVelo;
    private bool isCrashing = false;
    private Transform transf;
    private int wallLayer = 8;
    private Vector3 initPos;
    private Quaternion initRot;
    private Quaternion parentRot;

    void Start()
    {
        transf = transform;
        initPos = transf.position;
        initRot = transf.rotation;
        parentRot = transform.root.rotation;
    }

    void Update()
    {
        if (isCrashing)
            transf.Translate(crashVelo * Mathf.Abs(speed), Space.World);
        else
            transf.Translate(transform.forward * speed, Space.World);
        Rotate();
        ActivateThruster();
        Shoot();
        if (Input.GetButtonDown("Cancel"))
            ReturnShip();
    }

    void ActivateThruster()
    {
        if (Input.GetButton("Jump"))
        {
            Accelerate();
            // if (particle.isStopped)
            //     particle.Play();
        }
        // if (Input.GetButtonUp("Jump"))
        //     particle.Stop();
        if (Input.GetButton("Action1"))
            SlowDown();
    }

    void SlowDown()
    {
        if (isCrashing)
        {
            speed += Time.deltaTime * chargeSpeed;
            if (speed > 0)
                isCrashing = false;
        }
        else
        {
            speed -= Time.deltaTime * chargeSpeed;
            speed = Mathf.Clamp(speed, 0, maxSpeed);
            currentVelo = transform.forward * speed;
        }
    }

    void Accelerate()
    {
        if (isCrashing)
        {
            speed += Time.deltaTime * chargeSpeed;
            if (speed > 0)
                isCrashing = false;
        }
        else
        {
            speed += Time.deltaTime * chargeSpeed;
            speed = Mathf.Clamp(speed, 0, maxSpeed);
            currentVelo = transform.forward * speed;
        }
    }

    void Rotate()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        Vector2 velocity = new Vector2(0, 0);
        if (xMove != 0 || yMove != 0)
        {
            Vector3 movement = new Vector3(-xMove, 0.0f, -yMove);
            Quaternion rotation = parentRot * Quaternion.LookRotation(movement);
            // Quaternion rotation = Quaternion.LookRotation(movement);
            // rotation *= Quaternion.Euler(0, 90, 0);
            transform.rotation = rotation;
        }
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Action2"))
        {
            Quaternion rotation = transf.rotation * parentRot;
            GameObject newProj = Instantiate(projectile, transf.position, rotation);
            newProj.GetComponent<Projectile>().SetDirection(transform.forward);
        }
    }

    public void ActivateShip()
    {
        gameObject.transform.SetParent(null);
        gameObject.layer = 0;
    }

    public void ReturnShip()
    {
        gameObject.transform.SetParent(parent);
        gameObject.layer = wallLayer;
        transf.position = initPos;
        transf.rotation = initRot;
        speed = 0;
    }
}
