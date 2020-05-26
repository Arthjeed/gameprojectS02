using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovementPlayer : MonoBehaviour
{
    public float speed = 10;
    private Animator anim;
    private Rigidbody2D rb;
    private GameObject parent;
    private Quaternion parentRot;
    private Rigidbody2D rbParent;
    private bool animationIsPlaying = false;
    private PhotonView PV;

    void Start()
    {
        anim = GetComponent<Animator>();
        parent = transform.parent.gameObject;
        parentRot = parent.transform.rotation;
        rbParent = parent.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (PV.IsMine || !PhotonNetwork.IsConnected)
            Move();
    }

    void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        Vector2 velocity = new Vector2(0, 0);
        if (xMove != 0 || yMove != 0)
        {
            Vector3 movement = new Vector3(xMove, 0.0f, yMove);
            Quaternion rotation = parentRot * Quaternion.LookRotation(movement);
            transform.rotation = rotation;
            velocity.x = xMove * speed * Time.deltaTime;
            velocity.y = yMove * speed * Time.deltaTime;
            rb.AddForce(velocity * speed);
            // transform.position += new Vector3(xMove * Time.deltaTime * speed, yMove * Time.deltaTime * speed, 0);
            StartCoroutine(PlayAnimation("Run"));
        }
        else// if (!animationIsPlaying)
            StartCoroutine(PlayAnimation("Idle"));
        // velocity += rbParent.velocity;
        // print(rbParent.velocity);
        // rb.velocity = velocity * Time.deltaTime * speed;
        // rb.AddForce(velocity * speed);
    }

    private IEnumerator PlayAnimation(string animName)
    {
        anim.Play(animName, 0);
        yield return new WaitForEndOfFrame();
        animationIsPlaying = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        animationIsPlaying = false;
    }

    public void PlayIdle() {
        StartCoroutine(PlayAnimation("Idle"));
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     print("Collision");
    //     print(collision.gameObject.tag);
    // }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     print("trigger");
    //     print(collision.gameObject.tag);
    // }
}
