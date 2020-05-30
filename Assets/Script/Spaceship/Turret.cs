using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    public string slideButton = "Move";
    public float rotateAngleSpeed = 10;
    public float minAngle = 15;
    public float maxAngle = 165;
    public GameObject projectile;
    private Vector2 position;
    private Vector2 localPosition;
    private GameObject parent;
    private Transform transf;
    private SpaceShip ship;
    private float angle = 90;
    private float rayon = 100;
    private Vector3 startPosition;
    private Quaternion parentRot;
    // private PhotonView PV;

    void Start()
    {
        // PV = GetComponent<PhotonView>();
        transf = transform;
        position = transf.position;
        localPosition = transf.localPosition;
        parent = transf.parent.gameObject;
        ship = parent.GetComponent<SpaceShip>();
        rayon = 0;//Vector2.Distance(position, parent.transform.position);
        startPosition = transform.localPosition;
        parentRot = transform.parent.transform.rotation;
    }

    void Update()
    {
        RotateThruster();
        ActivateTurret();
    }

    void RotateThruster()
    {
        position = transf.position;
        localPosition = transf.localPosition;
        float inputValue = Input.GetAxis("Horizontal");
        if (inputValue != 0)
        {
            // Vector2 parentPos = parent.transform.position;
            Vector2 parentPos = transf.localPosition;
            float angleAbs = Mathf.Abs(angle);
            // print(angle);
            if (angleAbs >= minAngle && angleAbs <= maxAngle)
            {
                angle = angle + (inputValue * Time.deltaTime * rotateAngleSpeed);
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * rayon;
                float y = Mathf.Sin(angle * Mathf.Deg2Rad) * rayon;
                transf.RotateAround(transf.position, Vector3.forward, inputValue * rotateAngleSpeed * Time.deltaTime);
                // transf.localPosition = new Vector3(x, y);
                // transf.localPosition = startPosition;
            }
            else
            {
                //print(angle);
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
    }

    void ActivateTurret()
    {
        if (Input.GetButtonDown("Jump"))
        {
            // print(transf.rotation);
            // print(transf.localRotation);
            // Vector3 rotation = transf.localRotation * Vector3.forward;
            Quaternion rotation = transf.rotation * parentRot;
            // GameObject newProj = Instantiate(projectile, transf.position, Quaternion.LookRotation(transf.forward, Vector3.up));
            // GameObject newProj = Instantiate(projectile, transf.position, Quaternion.identity * Quaternion.LookRotation(transf.forward, Vector3.up));
            GameObject newProj = PhotonNetwork.Instantiate("ProjectileTurret", transf.position, rotation);
            // newProj.transform.parent = gameObject.transform;
            newProj.GetComponent<Projectile>().SetDirection(transform.forward);
        }
    }
}
