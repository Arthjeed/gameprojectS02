using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public int socialDistancing;
    public Transform _target;

    public float speed;
    private float previousAngle;

    private void Start()
    {
    }

    void Update()
    {
        //followTarget(_target);
    }

    public void followTarget(Transform target)
    {
        transform.LookAt(target.position);

        if (Vector3.Distance(transform.position, target.position) > socialDistancing)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        if (transform.position.x - target.position.x < 0)
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -90);
        else
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, +90);
            
    }

}