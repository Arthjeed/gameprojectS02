using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather : MonoBehaviour {
    Transform player;
    private float distance;
    public int DistanceMax;
    Rigidbody2D move;
    // Use this for initialization
    void Start () {
        move = GetComponent<Rigidbody2D> ();
        player = GameObject.FindGameObjectWithTag ("Player").transform;
    }

    // Update is called once per frame
    void Update () {
        distance = Vector3.Distance (transform.position, player.transform.position);
        if (distance < DistanceMax) {
            move.AddForce ((player.transform.position - transform.position) );
        }

    }
}