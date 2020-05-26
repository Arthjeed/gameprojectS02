using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shield : MonoBehaviour
{
    public float rotateAngleSpeed = 10;
    private Vector2 position;
    private Vector2 localPosition;
    private GameObject parent;
    private SpaceShip ship;
    private float rayon = 100;
    private PhotonView PV;

    // private float angle = 0;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        position = transform.position;
        localPosition = transform.localPosition;
        parent = transform.parent.gameObject;
        ship = parent.GetComponent<SpaceShip>();
        rayon = Vector2.Distance(position, parent.transform.position);
        this.enabled = false;
    }

    void Update()
    {
        if (PV.IsMine || !PhotonNetwork.IsConnected)
            RotateShield();
    }

    void RotateShield()
    {
        position = transform.position;
        localPosition = transform.localPosition;
        float inputValue = Input.GetAxis("Horizontal");
        if (inputValue != 0)
        {
            Vector2 parentPos = parent.transform.position;
            transform.RotateAround(parentPos, Vector3.forward, inputValue * rotateAngleSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AllyProjectile"))
            PhotonNetwork.Destroy(collision.gameObject);
    }
}
