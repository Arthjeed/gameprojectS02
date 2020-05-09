using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShip : MonoBehaviour
{
    public GameObject obj;
    private Vector3 initPos;
    void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, initPos.z);
    }
}
