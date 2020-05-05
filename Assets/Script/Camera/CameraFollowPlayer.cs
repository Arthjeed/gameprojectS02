using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform spaceshipPos;
    public float transitionSpeed = 5;
    public float cameraSizePlayer = 40;
    public float cameraSizeShip = 120;
    public float speedLerpT = 1f;
    private Vector3 initPos;
    private float currentSize = 40;
    private float lerpT = 0;
    private bool cameraOnPlayer = true;
    private Camera cam;
    private Interact interact;
    void Start()
    {
        initPos = transform.position;
        cam = GetComponent<Camera>();
        interact = player.GetComponent<Interact>();
    }

    void Update() {
        if (Input.GetButtonDown("ZoomOut"))
           interact.SetView(false);
        if (Input.GetButtonUp("ZoomOut"))
            interact.SetView(true);
    }

    void FixedUpdate()
    {
        if (cameraOnPlayer) {
            Vector3 tmpPos = new Vector3(player.transform.position.x, player.transform.position.y, initPos.z);
            transform.position = Vector3.Lerp(transform.position, tmpPos, Time.deltaTime * transitionSpeed);
            currentSize = Mathf.Lerp(cameraSizeShip, cameraSizePlayer, lerpT);
            if (lerpT < 1)
                lerpT += speedLerpT * Time.deltaTime;
            cam.orthographicSize = currentSize;
        }
        if (!cameraOnPlayer) {
            transform.position = Vector3.Lerp(transform.position, spaceshipPos.position, Time.deltaTime * transitionSpeed);
            currentSize = Mathf.Lerp(cameraSizePlayer, cameraSizeShip, lerpT);
            cam.orthographicSize = currentSize;
            if (lerpT < 1)
                lerpT += speedLerpT * Time.deltaTime;
        }
    }

    // void LateUpdate() {
    //     if (!cameraOnPlayer)
    //     {
    //         transform.position = Vector3.Lerp(transform.position, spaceshipPos.position, Time.deltaTime * transitionSpeed);
    //         currentSize = Mathf.Lerp(cameraSizePlayer, cameraSizeShip, lerpT);
    //         cam.orthographicSize = currentSize;
    //         if (lerpT < 1)
    //             lerpT += speedLerpT * Time.deltaTime;
    //     }
    // }

    public void SwitchCamera() {
        // if (lerpT > 1)
        // {
            lerpT = 0;
            cameraOnPlayer = !cameraOnPlayer;
            if (!cameraOnPlayer)
                cam.cullingMask = (1 << LayerMask.NameToLayer("Default"));// | (1 << LayerMask.NameToLayer("Wall"));
            else
                cam.cullingMask = -1;
        // }
    }
}
