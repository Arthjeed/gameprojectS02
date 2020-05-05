using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private float speed;

    public void initSpeed(float _speed)
    {
        speed = _speed;
    }

    public void followTarget(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) > 50)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        transform.LookAt(target.position);
        Debug.Log(transform.localEulerAngles);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 270, 90);
        Debug.Log(transform.localEulerAngles);

    }
}