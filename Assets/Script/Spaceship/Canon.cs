using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Canon : MonoBehaviour
{
    public GameObject projectile;
    private GameObject parent;
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }
    }

    public void Shoot() {
        Vector2 parentPos = parent.transform.position;
        Vector2 position = transform.position;
        Vector2 dir = position - parentPos;
        Quaternion rotation = transform.rotation;// * parent.transform.rotation;
        GameObject newProj = PhotonNetwork.Instantiate("ProjectileCanon", position, rotation);
        newProj.GetComponent<Projectile>().SetDirection(dir);
    }
}
