using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public Vector2 maxVelocity = new Vector2(1, 1);
    public float maxSpeed = 5;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyVelocity(Vector2 velocity) {
        Vector2 tmpVelocity = rb.velocity + velocity;
        float signX = Mathf.Sign(tmpVelocity.x);
        float signY = Mathf.Sign(tmpVelocity.y);
        // print(tmpVelocity);
        tmpVelocity.x = Mathf.Clamp(Mathf.Abs(tmpVelocity.x), 0, maxVelocity.x);
        tmpVelocity.x = signX * tmpVelocity.x;
        tmpVelocity.y = Mathf.Clamp(Mathf.Abs(tmpVelocity.y), 0, maxVelocity.y);
        tmpVelocity.y = signY * tmpVelocity.y;
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
        rb.velocity = tmpVelocity;
    }
}
