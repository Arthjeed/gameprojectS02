using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShipBehavior : MonoBehaviour
{
    private EnemyShipMovement shipMovement;
    private FollowPlayer follow;
    private Ray ray;
    private Transform player;

    private float life;
    private float damage;

    private float dmgAnimeLapse = 0.1f;
    private float dmgAnimeCount = 0;
    private bool damaged = false;
    private float dirMaxTime = 100;
    private float dirTimer = 50;
    private int strafeDir = 0;

    public int ShipAILevel = 1;
    public int ShipPower = 2;
    public GameObject dropUranium;
    public GameObject dropHealth;
    public GameObject deathAnimation;
    public ParticleSystem damageSmoke;
    public Material damageTexture;
    private Material originalTexture;

    void Start()
    {
        shipMovement = GetComponent<EnemyShipMovement>();
        follow = GetComponent<FollowPlayer>();
        player = GameObject.FindGameObjectWithTag("Spaceship").transform;
        //player = GameObject.FindGameObjectsWithTag("player");
        life = ShipPower * 6;
        shipMovement.setDamage(ShipPower * 2);
        originalTexture = GetComponent<Renderer>().material;
        damageSmoke.Stop();
        strafeDir = Random.Range(-1, 2);
        strafeDir = (strafeDir == 0) ? -1 : 1;
    }

    void Update()
    {
        checkDistance();
        //shipMovement.strafe(transform.right);
        //player.position += (Vector3.up /50);
        if (damaged)
            checkDmgAnimation();

        if (ShipAILevel == 3)
        {
            dirTimer--;
            if (dirTimer < 0)
            {
                strafeDir = Random.Range(-1, 2);
                dirTimer = dirMaxTime;
            }
        }

    }

    void checkDistance()
    {
        if (Vector3.Distance(transform.position, player.position) < 500)
        {
            if (Vector3.Distance(transform.position, player.position) < follow.socialDistancing)
            {
                switch (ShipAILevel)
                {
                    case 1:
                        shipMovement.shoot();
                        follow.followTarget(player);
                        break;
                    case 2:
                        shipMovement.shoot();
                        shipMovement.strafe(strafeDir, player);
                        break;
                    case 3:
                        shipMovement.shoot();
                        shipMovement.strafe(strafeDir, player);
                        break;
                }
                   
                shipMovement.reactorShutDown();
            }
            else 
            {
                follow.followTarget(player);
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

    public void TakeDamage(float playerDamage)
    {
        life -= playerDamage;
        GetComponent<Renderer>().material = damageTexture;
        dmgAnimeCount = dmgAnimeLapse;
        damaged = true;
        damageSmoke.Play();
        if (life <= 0)
            DestroyShip();
    }

    private void checkDmgAnimation()
    {
        if (dmgAnimeCount > 0)
        {
            dmgAnimeCount -= Time.deltaTime;
            if (dmgAnimeCount <= 0)
                GetComponent<Renderer>().material = originalTexture;

        }
    }

    void DestroyShip()
    {
        int sizeDrop = Random.Range(ShipPower - 1, ShipPower + 2);
        int typeDrop = Random.Range(1, 3);

        GameObject animation = Instantiate(deathAnimation, transform.position, Random.rotation);
        animation.transform.localScale = new Vector3(10, 10, 10);
        if (sizeDrop >= 1 && typeDrop != 0)
        {
            GameObject drop = dropUranium;
            if (typeDrop == 2)
                drop = dropHealth;
            GameObject newDrop = Instantiate(drop, transform.position, transform.rotation);
            newDrop.GetComponent<DropBehavior>().setValue(sizeDrop);
        }
        Destroy(gameObject);
    }
}
