using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniShipPoint : MonoBehaviour
{
    public float arrowPadding = 20;
    private Transform spaceship;
    private float rotateSpeed = 100;
    private Quaternion initRot;
    private Transform parent;
    void Awake()
    {
        spaceship = GameObject.Find("Spaceship").transform;
        initRot = transform.rotation;
        parent = transform.parent;
    }

    void Update()
    {
        Vector3 dir = spaceship.position - parent.position;
        dir.z = 0;
        // Debug.DrawRay(parent.position, dir, Color.red);
        dir = Vector3.Normalize(dir);
        transform.position = parent.position + (dir * arrowPadding);
        Vector3 newDirection = Vector3.RotateTowards(Vector3.forward, spaceship.position, rotateSpeed * Time.deltaTime, 0);
        transform.rotation = initRot * Quaternion.LookRotation(newDirection);
        // Vector3 newDirection = Vector3.RotateTowards(parent.position, dir, rotateSpeed * Time.deltaTime, 0);
        // transform.rotation = initRot * Quaternion.LookRotation(newDirection);
    }
}
