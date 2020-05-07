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
        player.position += (Vector3.up /50);
    }

    void checkDistance()
    {
        if (Vector3.Distance(transform.position, player.position) < 500)
        {
            if (Vector3.Distance(transform.position, player.position) < 50)
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
     //dodge(transform.forward, 0);
    }

/*    void dodge(Vector3 direction, int loop)
    {
        if (loop == 100)
            return;
        print(loop);
        loop++;
        ray.origin = transform.position;
        ray.direction = (transform.forward + (direction * (loop/ 100)));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Wall")
            {
                if (loop % 2 == 1)
                    dodge(transform.right, loop);
                else
                    dodge(-transform.right, loop);
            }
            else
            {
                print(loop);
                shipMovement.goForward(0.5f);
            }
        }
    }*/
}
