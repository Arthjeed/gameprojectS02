using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBehavior : MonoBehaviour
{
    private EnemyShipMovement shipMovement;
    private FollowPlayer follow;
    private Ray ray;
    private Transform player;

    private int RIGHT = 1;
    private int LEFT = -1;

    public int ShipAILevel = 1;
    public int ShipPower = 2;
    public GameObject drop;

    void Start()
    {
        shipMovement = GetComponent<EnemyShipMovement>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        follow = GetComponent<FollowPlayer>();
        follow.initSpeed(shipMovement.maxSpeed);
    }

    void Update()
    {
        checkDistance();
        //shipMovement.strafe(transform.right);
        //player.position += (Vector3.up /50);
    }

    void checkDistance()
    {
        if (Vector3.Distance(transform.position, player.position) < 500)
        {
            if (Vector3.Distance(transform.position, player.position) < 150)
            {
                switch (ShipAILevel)
                {
                    case 1:
                        shipMovement.shoot();
                        break;
                    case 2:
                        shipMovement.shoot();
                        shipMovement.strafe(LEFT, player);
                        break;
                }
                   
                shipMovement.reactorShutDown();
            }
            else 
            {
                GetComponent<FollowPlayer>().followTarget(player);
                shipMovement.reactorIgnit();
            }
        }
    }

    void explore()
    {
     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            DestroyShip();
        }
    }

    void DestroyShip()
    {
        int sizeDrop = Random.Range(ShipPower - 1, ShipPower + 2);

        if (sizeDrop >= 1)
        {
            GameObject newDrop = Instantiate(drop, transform.localPosition, transform.rotation);
            //newDrop.transform.localScale = new Vector3(sizeDrop, sizeDrop, sizeDrop);
            newDrop.GetComponent<DropBehavior>().setValue(sizeDrop);
        }
        Destroy(gameObject);
    }
}
