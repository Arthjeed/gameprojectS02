using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Transform spaceship;
    void Start()
    {
        spaceship = GameObject.Find("Spaceship").transform;
    }

    void LateUpdate()
    {
        Vector3 newPos = spaceship.position;
        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}
