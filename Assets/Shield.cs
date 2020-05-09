using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float rotateAngleSpeed = 10;
    private Vector2 position;
    private Vector2 localPosition;
    private GameObject parent;
    private SpaceShip ship;
    private float rayon = 100;
    private ParticleSystem particle;

    // private float angle = 0;

    void Start()
    {
        position = transform.position;
        localPosition = transform.localPosition;
        parent = transform.parent.gameObject;
        ship = parent.GetComponent<SpaceShip>();
        rayon = Vector2.Distance(position, parent.transform.position);
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
        this.enabled = false;
    }

    void Update()
    {
        position = transform.position;
        localPosition = transform.localPosition;
        float x = Input.GetAxis("Horizontal");
        if (x != 0)
            RotateShield(x);
        // ActivateThruster();
    }

    void RotateShield(float inputValue)
    {
        Vector2 parentPos = parent.transform.position;
        transform.RotateAround(parentPos, Vector3.forward, inputValue * rotateAngleSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger");
        print(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("AllyProjectile"))
            Destroy(collision.gameObject);
    }

    // void ActivateThruster()
    // {
    //     if (Input.GetButton("Jump"))
    //     {
    //         ship.Accelerate();
    //         if (particle.isStopped)
    //             particle.Play();
    //     }
    //     if (Input.GetButtonUp("Jump"))
    //         particle.Stop();
    //     if (Input.GetButton("Action1"))
    //         ship.SlowDown();
    //     // if (Input.GetButtonDown("Jump"))
    //     // {
    //     //     // Vector2 parentPos = parent.transform.position;
    //     //     // Vector2 dir = parentPos - position;
    //     //     // Vector2 power = new Vector2(dir.x * thrusterPower.x, dir.y * thrusterPower.y);
    //     //     // Debug.Log(dir);
    //     //     // ship.ApplyVelocity(power);
    //     //     buttonIsDown = true;
    //     // }
    //     // if (Input.GetButtonUp("Jump"))
    //     //     buttonIsDown = false;

    //     // if (buttonIsDown)
    //     // {
    //     //     // Vector2 power = new Vector2(0, 0);
    //     //     // ship.ApplyVelocity(power);
    //     //     ship.Accelerate();
    //     // }
    // }
}
