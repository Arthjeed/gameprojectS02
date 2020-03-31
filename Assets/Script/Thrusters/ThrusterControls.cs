using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterControls : MonoBehaviour
{
    // Start is called before the first frame update
    public string slideButton = "Move";
    public float rotateAngleSpeed = 100;
    public Vector2 thrusterPower = new Vector2(0.1f, 0.1f);
    private Vector2 position;
    private GameObject parent;
    private SpaceShip ship;

    void Start()
    {
        position = transform.position;
        parent = transform.parent.gameObject;
        ship = parent.GetComponent<SpaceShip>();
    }

    void Update()
    {
        position = transform.position;
        float x = Input.GetAxis("Horizontal");
        if (x != 0)
            RotateThruster(x);
        ActivateThruster();
    }

    void RotateThruster(float inputValue) {
        Vector2 parentPos = parent.transform.position;
        transform.RotateAround(parentPos, Vector3.forward, inputValue * rotateAngleSpeed * Time.deltaTime);
        // else if (x < 0)
        // {
        //     Vector2 parentPos = parent.transform.position;
        //     transform.RotateAround(parentPos, Vector3.forward, rotateAngleSpeed * Time.deltaTime);
        //     Vector2 dir = parentPos - position;
        //     ship.ApplyVelocity(dir);
        // }
    }

    void ActivateThruster() {
        if (Input.GetButtonDown("Jump")) {
            Vector2 parentPos = parent.transform.position;
            Vector2 dir = parentPos - position;
            Vector2 power = new Vector2(dir.x * thrusterPower.x, dir.y * thrusterPower.y);
            print("before");
            print(power);
            ship.ApplyVelocity(power);
        }
    }
}
