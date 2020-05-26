using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private float speed;
    private float previousAngle;

    public void initSpeed(float _speed)
    {
        speed = _speed;
    }

    public void followTarget(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) > 150)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target.position);

        lookTarget(target);
        if (transform.position.x - target.position.x < 0)
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -90);
        else
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, +90);
            
    }

    public void lookTarget(Transform target)
    {


        /*        if (transform.localEulerAngles.x > previousAngle)
                    transform.localEulerAngles = new Vector3(previousAngle + (transform.localEulerAngles.x - 180), 270, 90);
                else*/
    }
}