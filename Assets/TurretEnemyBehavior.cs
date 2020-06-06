using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyBehavior : MonoBehaviour
{
    public Transform pivot;
    public GameObject AsteroideParent;

    public int ShipAILevel = 1;
    public int ShipPower = 2;
    public GameObject dropUranium;
    public GameObject dropHealth;
    public GameObject deathAnimation;

    private Transform player;
    private Transform tmpLook;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        LookPlayer();
    }

    void LookPlayer()
    {
        //pivot.transform.LookAt(player.position);

        Vector2 tmp = new Vector2(player.localPosition.x, player.localPosition.y);
        float angle = AngleTo(new Vector2(pivot.transform.position.x, pivot.transform.position.y), tmp);

        //print(angle);
        //        float rot_z = Mathf.Atan2(player.position.y, player.position.x) * Mathf.Rad2Deg;
        pivot.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        /*        if (player.transform.position.x < pivot.transform.position.x)
                    pivot.transform.localEulerAngles = new Vector3(tmpLook.localEulerAngles.x + 40, 0, 0);
                else
                    pivot.transform.localEulerAngles = new Vector3(180 - tmpLook.localEulerAngles.x + 40, 0, 0);*/

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
