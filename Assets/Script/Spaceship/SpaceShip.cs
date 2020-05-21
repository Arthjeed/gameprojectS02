using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float maxSpeed = 20;
    public float chargeSpeed = 10f;
    public float crashSlowSpeed = 0.4f;
    public float speed = 0;
    public GameObject playerPrefab;
    private Vector3 currentVelo;
    private Vector3 crashVelo; 
    private Transform transf;
    private bool isCrashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transf = transform;
        InstantiatePlayer();
    }

    void InstantiatePlayer()
    {
        Vector3 playerPos = new Vector3(0, 32.6f, 0); // position to be inside the spaceship at the right height
        GameObject player = Instantiate(playerPrefab, playerPos, transf.rotation); // give player prefab and spaceship rotation, pos is not important
        player.transform.parent = transf; // give spaceship as parent
        player.transform.localPosition = playerPos; // give the local position for the player
    }

    void Update()
    {
        if (isCrashing)
            transf.Translate(crashVelo * Mathf.Abs(speed), Space.World);
        else
            transf.Translate(transform.forward * speed, Space.World);
        // else
        //     transf.Translate(currentVelo, Space.World);
        // rb.velocity = transform.forward * Time.deltaTime * speed;
        // rb.AddForce(transform.forward * Time.deltaTime * speed);
    }

    public void SlowDown()
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

    public void Accelerate()
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

    public void Crash()
    {
        isCrashing = true;
        speed = -speed;
        crashVelo = transform.forward * speed * crashSlowSpeed;
    }

    public void ApplyVelocity(Vector2 velocity)
    {
        // Vector2 tmpVelocity = rb.velocity + velocity;
        // float signX = Mathf.Sign(tmpVelocity.x);
        // float signY = Mathf.Sign(tmpVelocity.y);
        // tmpVelocity.x = Mathf.Clamp(Mathf.Abs(tmpVelocity.x), 0, maxVelocity.x);
        // tmpVelocity.x = signX * tmpVelocity.x;
        // tmpVelocity.y = Mathf.Clamp(Mathf.Abs(tmpVelocity.y), 0, maxVelocity.y);
        // tmpVelocity.y = signY * tmpVelocity.y;
        // if (signX == Mathf.Sign(velocity.x) && velocity.x * maxSpeed > tmpVelocity.x) {
        //     tmpVelocity.x = Mathf.Clamp(Mathf.Abs(tmpVelocity.x), 0, Mathf.Abs(velocity.x) * maxSpeed);
        //     tmpVelocity.x = signX * tmpVelocity.x;
        // }
        // else if (signX == Mathf.Sign(velocity.x))
        // {
        //     tmpVelocity.x = Mathf.Clamp(Mathf.Abs(tmpVelocity.x), 0, 0.2f * maxSpeed);
        //     tmpVelocity.x = signX * tmpVelocity.x;
        // }
        // if (signY == Mathf.Sign(velocity.y) && velocity.y * maxSpeed > tmpVelocity.y)
        // {
        //     tmpVelocity.y = Mathf.Clamp(Mathf.Abs(tmpVelocity.y), 0, Mathf.Abs(velocity.y) * maxSpeed);
        //     tmpVelocity.y = signY * tmpVelocity.y;
        // }
        // else if (signY == Mathf.Sign(velocity.y))
        // {
        //     tmpVelocity.y = Mathf.Clamp(Mathf.Abs(tmpVelocity.y), 0, 0.2f * maxSpeed);
        //     tmpVelocity.y = signY * tmpVelocity.y;
        // }
        // if (signY == Mathf.Sign(velocity.y)) {
        //     tmpVelocity.y = Mathf.Clamp(Mathf.Abs(tmpVelocity.y), 0, 0.2f > Mathf.Abs(velocity.y) ? Mathf.Abs(velocity.y) * maxSpeed : 0.2f * maxSpeed);
        //     tmpVelocity.y = signY * tmpVelocity.y;
        // }
         
        // if (Mathf.Sign(velocity.x) == Mathf.Sign(tmpVelocity.x) && Mathf.Sign(velocity.x) >= 0)
        //     tmpVelocity.x = Mathf.Clamp(tmpVelocity.x, -Mathf.Abs(velocity.x * maxSpeed), velocity.x * maxSpeed);
        // if (Mathf.Sign(velocity.y) == Mathf.Sign(tmpVelocity.y) && Mathf.Sign(velocity.y) >= 0)
        //     tmpVelocity.y = Mathf.Clamp(tmpVelocity.y, -Mathf.Abs(velocity.y * maxSpeed), velocity.y * maxSpeed);
        // print("after");
        // print(tmpVelocity);
        // rb.velocity = tmpVelocity;
    }
}
