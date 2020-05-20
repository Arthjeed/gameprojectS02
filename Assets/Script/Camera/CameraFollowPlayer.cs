using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject spaceship;
    private Transform spaceshipPos;
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
    private SpaceShip spaceshipInfo;
    void Start()
    {
        initPos = transform.position;
        cam = GetComponent<Camera>();
        interact = player.GetComponent<Interact>();
        spaceshipPos = spaceship.transform;
        spaceshipInfo = spaceship.GetComponent<SpaceShip>();
    }

    void Update()
    {
        if (Input.GetButtonDown("ZoomOut") && !interact.interacting)
            interact.SetView(false);
        if (Input.GetButtonUp("ZoomOut") && !interact.interacting)
            interact.SetView(true);
        if (cameraOnPlayer)
        {
            Vector3 tmpPos = new Vector3(player.transform.position.x, player.transform.position.y, initPos.z);

            if (lerpT < 1)
            {
                transform.position = Vector3.Lerp(transform.position, tmpPos, Time.deltaTime * (transitionSpeed + Mathf.Abs(spaceshipInfo.speed) * 10));
                currentSize = Mathf.Lerp(cameraSizeShip, cameraSizePlayer, lerpT);
                lerpT += speedLerpT * Time.deltaTime;
                cam.orthographicSize = currentSize;
            }
            else
                transform.position = tmpPos;
        }
        if (!cameraOnPlayer)
        {
            Vector3 tmpPos = new Vector3(spaceshipPos.position.x, spaceshipPos.position.y, initPos.z);

            if (lerpT < 1)
            {
                transform.position = Vector3.Lerp(transform.position, tmpPos, Time.deltaTime * (transitionSpeed + Mathf.Abs(spaceshipInfo.speed) * 10));
                currentSize = Mathf.Lerp(cameraSizePlayer, cameraSizeShip, lerpT);
                cam.orthographicSize = currentSize;
                lerpT += speedLerpT * Time.deltaTime;
            }
            else
                transform.position = tmpPos;
        }
    }

    void FixedUpdate()
    {
        // if (cameraOnPlayer)
        // {
        //     Vector3 tmpPos = new Vector3(player.transform.position.x, player.transform.position.y, initPos.z);

        //     if (lerpT < 1)
        //     {
        //         transform.position = Vector3.Lerp(transform.position, tmpPos, Time.deltaTime * (transitionSpeed + Mathf.Abs(spaceshipInfo.speed) * 10));
        //         currentSize = Mathf.Lerp(cameraSizeShip, cameraSizePlayer, lerpT);
        //         lerpT += speedLerpT * Time.deltaTime;
        //     }
        //     else
        //         transform.position = tmpPos;
        //     // if (lerpT < 1)
        //     cam.orthographicSize = currentSize;
        // }
        // if (!cameraOnPlayer)
        // {
        //     Vector3 tmpPos = new Vector3(spaceshipPos.position.x, spaceshipPos.position.y, initPos.z);

        //     if (lerpT < 1)
        //     {
        //         transform.position = Vector3.Lerp(transform.position, tmpPos, Time.deltaTime * (transitionSpeed + Mathf.Abs(spaceshipInfo.speed) * 10));
        //         currentSize = Mathf.Lerp(cameraSizePlayer, cameraSizeShip, lerpT);
        //         cam.orthographicSize = currentSize;
        //         lerpT += speedLerpT * Time.deltaTime;
        //     }
        //     else
        //         transform.position = tmpPos;
            
        //     // if (lerpT < 1)
        // }
    }

    void LateUpdate()
    {
        // if (cameraOnPlayer)
        // {
        //     Vector3 tmpPos = new Vector3(player.transform.position.x, player.transform.position.y, initPos.z);
        //     transform.position = Vector3.Lerp(transform.position, tmpPos, Time.deltaTime * (transitionSpeed + Mathf.Abs(spaceshipInfo.speed) * 10));
        //     currentSize = Mathf.Lerp(cameraSizeShip, cameraSizePlayer, lerpT);
        //     if (lerpT < 1)
        //         lerpT += speedLerpT * Time.deltaTime;
        //     cam.orthographicSize = currentSize;
        // }
        // if (!cameraOnPlayer)
        // {
        //     Vector3 tmpPos = new Vector3(spaceshipPos.position.x, spaceshipPos.position.y, initPos.z);
        //     transform.position = Vector3.Lerp(transform.position, tmpPos, Time.deltaTime * (transitionSpeed + Mathf.Abs(spaceshipInfo.speed) * 10));
        //     currentSize = Mathf.Lerp(cameraSizePlayer, cameraSizeShip, lerpT);
        //     cam.orthographicSize = currentSize;
        //     if (lerpT < 1)
        //         lerpT += speedLerpT * Time.deltaTime;
        // }
    }

    public void SwitchCamera()
    {
        lerpT = 0;
        cameraOnPlayer = !cameraOnPlayer;
        if (!cameraOnPlayer)
            cam.cullingMask = (1 << LayerMask.NameToLayer("Default"));// | (1 << LayerMask.NameToLayer("Wall"));
        else
            cam.cullingMask = -1;
    }
}
