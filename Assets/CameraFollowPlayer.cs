using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    
    public GameObject obj;
    private Vector3 initPos;
    private Camera cam;
    void Start()
    {
        initPos = transform.position;
        cam = GetComponent<Camera>();
        // cam.cullingMask = 1 << 0;
    }

    void Update()
    {
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, initPos.z);
    }
}
