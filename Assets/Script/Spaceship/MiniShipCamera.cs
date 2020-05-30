﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniShipCamera : MonoBehaviour
{
    private Vector3 initPos;
    private Camera cam;
    private Quaternion rotation;
    void Start()
    {
        initPos = transform.position;
        cam = GetComponent<Camera>();
        cam.cullingMask = (1 << LayerMask.NameToLayer("Default"));
    }

    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
    }

    void Update()
    {
        Vector3 parentPos = transform.parent.position;
        transform.position = new Vector3(parentPos.x, parentPos.y, initPos.z);
    }
}
