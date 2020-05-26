using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraFollowPlayer : MonoBehaviour
{
    public float transitionSpeed = 5;
    public float cameraSizePlayer = 40;
    public float cameraSizeShip = 120;
    public float speedLerpT = 1f;
    private GameObject spaceship;
    private Transform spaceshipPos;
    private Vector3 initPos;
    private float currentSize = 40;
    private float lerpT = 0;
    private bool cameraOnPlayer = true;
    private Camera cam;
    private Interact interact;
    private SpaceShip spaceshipInfo;
    private Transform player;
    private Quaternion rotation;
    private PhotonView PV;
    void Start()
    {
        player = transform.parent.GetChild(0);
        PV = player.GetComponent<PhotonView>();
        if (!PV.IsMine && PhotonNetwork.IsConnected)
            gameObject.SetActive(false);

        cam = GetComponent<Camera>();
        initPos = transform.position;
        interact = player.GetComponent<Interact>();
        spaceship = transform.root.gameObject;
        spaceshipPos = spaceship.transform;
        spaceshipInfo = spaceship.GetComponent<SpaceShip>();
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
        UpdateCamera();
    }

    void UpdateCamera()
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
