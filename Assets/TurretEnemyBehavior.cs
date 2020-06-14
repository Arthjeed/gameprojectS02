using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretEnemyBehavior : MonoBehaviour
{
    public Transform pivot;
    public GameObject AsteroideParent;

    public int ShipAILevel = 1;
    public int ShipPower = 2;
    public float maxSpeed = 20;
    public float damage = 10;
    public float speedMissile = 0.2f;
    public int reloadTime = 20;
    private int reloadState = 0;

    public GameObject dropUranium;
    public GameObject dropHealth;
    public GameObject deathAnimation;
    public GameObject ShootingPoint;
    public GameObject ShootMissile;

    private Transform player;
    private Transform tmpLook;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Spaceship").transform;
        tmpLook = pivot;
    }

    private float AngleTo(Vector2 this_, Vector2 to)
    {
        Vector2 direction = to - this_;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0f) angle += 360f;
        return angle;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 500)
        {
            LookPlayer();
            shoot();
        }
    }

    void LookPlayer()
    {
        //pivot.transform.LookAt(player.position);

        Vector2 tmp = new Vector2(player.localPosition.x, player.localPosition.y);
        float angle = AngleTo(new Vector2(pivot.transform.position.x, pivot.transform.position.y), tmp);

        //print(angle -90);
        //        float rot_z = Mathf.Atan2(player.position.y, player.position.x) * Mathf.Rad2Deg;
        pivot.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        /*        if (player.transform.position.x < pivot.transform.position.x)
                    pivot.transform.localEulerAngles = new Vector3(tmpLook.localEulerAngles.x + 40, 0, 0);
                else
                    pivot.transform.localEulerAngles = new Vector3(180 - tmpLook.localEulerAngles.x + 40, 0, 0);*/

    }

    public void shoot()
    {

        reloadState++;
        if (reloadState == reloadTime)
        {
            reloadState = 0;
            Quaternion rotation;
            Vector2 dir = new Vector2(transform.forward.x, transform.forward.y);

            if (player.transform.localPosition.x < transform.localPosition.x)
                rotation = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.x + 270));
            else
                rotation = Quaternion.Euler(new Vector3(0, 0, 360 - transform.eulerAngles.x + 90));

            GameObject newProj = PhotonNetwork.Instantiate("ProjectileEnemy", ShootingPoint.transform.position, rotation);
            newProj.GetComponent<LaserBehavior>().setDirection(dir);
            newProj.GetComponent<LaserBehavior>().setValue(damage, speedMissile);

        }
    }

    public void DestroyShip()
    {
        int sizeDrop = Random.Range(ShipPower - 1, ShipPower + 2);
        int typeDrop = Random.Range(1, 3);
        print(typeDrop);

        GameObject animation = Instantiate(deathAnimation, transform.localPosition, Random.rotation);
        animation.transform.localScale = new Vector3(10, 10, 10);
        if (sizeDrop >= 1 && typeDrop != 0)
        {
            GameObject drop = dropUranium;
            if (typeDrop == 2)
                drop = dropHealth;
            GameObject newDrop = Instantiate(drop, transform.localPosition, transform.rotation);
            newDrop.GetComponent<DropBehavior>().setValue(sizeDrop);
        }
        Destroy(AsteroideParent);
    }

 
}
