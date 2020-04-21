using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShip : MonoBehaviour
{
    public GameObject obj;
    private Camera cam;
    private Vector3 initPos;
    void Start()
    {
        initPos = transform.position;
        cam = GetComponent<Camera>();
        cam.cullingMask = (1 << LayerMask.NameToLayer("Default"));// | (1 << LayerMask.NameToLayer("Wall"));
    }

    void Update()
    {
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, initPos.z);
    }
}
