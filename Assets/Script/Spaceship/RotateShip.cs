using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShip : MonoBehaviour
{
    public float rotateAngleSpeed = 10;
    private Transform transf;
    void Start()
    {
        transf = transform;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        if (x != 0)
            Rotate(-x);
    }

    public void Rotate(float inputValue)
    {
        transform.RotateAround(transf.position, Vector3.forward, inputValue * rotateAngleSpeed * Time.deltaTime);
    }
}
