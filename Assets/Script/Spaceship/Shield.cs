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
    private PhotonTransformView photonTrans;
    [SerializeField]
    private ParticleSystem indicator;

    // private float angle = 0;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        position = transform.position;
        localPosition = transform.localPosition;
        parent = transform.parent.gameObject;
        ship = parent.GetComponent<SpaceShip>();
        rayon = Vector2.Distance(position, parent.transform.position);
    }

    void OnEnable()
    {
        PV.RPC("StartShield", RpcTarget.All);
        indicator.Play();
        StartCoroutine(StopParticleSystem(indicator, 1.5f));
    }

    void OnDisable()
    {
        PV.RPC("StopShield", RpcTarget.All);
    }

    IEnumerator StopParticleSystem(ParticleSystem particleSystem, float time)
    {
        yield return new WaitForSeconds(time);
        particleSystem.Stop();
    }

    [PunRPC]
    void StartShield()
    {
        if (!photonTrans)
            photonTrans = GetComponent<PhotonTransformView>();
        photonTrans.enabled = true;
    }

    [PunRPC]
    void StopShield()
    {
        if (!photonTrans)
            photonTrans = GetComponent<PhotonTransformView>();
        photonTrans.enabled = false;
    }


    void Update()
    {
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
        if (collision.gameObject.CompareTag("EnemyProjectile"))
            PhotonNetwork.Destroy(collision.gameObject);
    }
}
