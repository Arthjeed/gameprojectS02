using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehavior : MonoBehaviour
{
    Vector3 rotationDirection = new Vector3(0, 0, 0);
    private float speedRotation = 1.0f;

    public float value;

    void Start()
    {
        
    }

    void Update()
    {
        rotationDirection[Random.Range(0, 3)] += 1.0f / (-100 | + 100);
        float singleStep = speedRotation * Time.deltaTime;

        transform.Rotate(Vector3.RotateTowards(transform.forward, rotationDirection, singleStep, 0.0f));
    }

    public void setValue(float _value)
    {
        transform.localScale = new Vector3(_value, _value, _value);
        value = _value;
    }
}
