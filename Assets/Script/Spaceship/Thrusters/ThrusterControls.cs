using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterControls : MonoBehaviour
{
    // Start is called before the first frame update
    public string slideButton = "Move";
    public float rotateAngleSpeed = 10;
    public Vector2 thrusterPower = new Vector2(0.1f, 0.1f);
    public float minAngle = 15;
    public float maxAngle = 165;
    private Vector2 position;
    private Vector2 localPosition;
    private GameObject parent;
    private SpaceShip ship;
    private float angle = 90;
    private float rayon = 100;
    private bool buttonIsDown = true;
    private KeyCode interactKey = KeyCode.E;
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
        // float x = Input.GetAxis("Horizontal");
        // if (x != 0)
        //     RotateThruster(x);
        ActivateThruster();
    }

    void RotateThruster(float inputValue) {
        Vector2 parentPos = parent.transform.position;
        // angle = Mathf.Atan2(parentPos.y - position.y, parentPos.x - position.x) * (180 / Mathf.PI);
        float angleAbs = Mathf.Abs(angle);
        print(angle);
        if (angleAbs >= minAngle && angleAbs <= maxAngle)
        {
            angle = angle + (inputValue * Time.deltaTime * rotateAngleSpeed);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * rayon;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * rayon;
            transform.RotateAround(parentPos, Vector3.forward, inputValue * rotateAngleSpeed * Time.deltaTime);
            transform.localPosition = new Vector2(x, y);
        } 
        else
        {
            angleAbs = Mathf.Clamp(angleAbs, minAngle, maxAngle);
            angle = angleAbs * Mathf.Sign(angle);
            // float rayon = Vector2.Distance(position, parentPos);
            // angleAbs = Mathf.Clamp(angleAbs, minAngle, maxAngle);
            // float x = rayon * Mathf.Cos(Mathf.Sign(angle) * angleAbs * Mathf.Deg2Rad);
            // float y = rayon * Mathf.Sin(Mathf.Sign(angle) * angleAbs * Mathf.Deg2Rad);
            // print(new Vector2(x, y));
            // transform.position = new Vector2(x, y);
        }
    }

    void ActivateThruster()
    {
        if (Input.GetButton("Jump"))
        {
            ship.Accelerate();
            if (particle.isStopped)
                particle.Play();
        }
        if (Input.GetButtonUp("Jump"))
            particle.Stop();
        if (Input.GetButton("Action1"))
            ship.SlowDown();
        // if (Input.GetButtonDown("Jump"))
        // {
        //     // Vector2 parentPos = parent.transform.position;
        //     // Vector2 dir = parentPos - position;
        //     // Vector2 power = new Vector2(dir.x * thrusterPower.x, dir.y * thrusterPower.y);
        //     // Debug.Log(dir);
        //     // ship.ApplyVelocity(power);
        //     buttonIsDown = true;
        // }
        // if (Input.GetButtonUp("Jump"))
        //     buttonIsDown = false;
        
        // if (buttonIsDown)
        // {
        //     // Vector2 power = new Vector2(0, 0);
        //     // ship.ApplyVelocity(power);
        //     ship.Accelerate();
        // }
    }
}
