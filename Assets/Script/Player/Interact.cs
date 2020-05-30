using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Interact : MonoBehaviour
{
    // public GameObject playerCamera;
    public GameObject playerCamera;
    private CameraFollowPlayer cam;
    private Post interactObject;
    private KeyCode cancelKey = KeyCode.R;
    private MonoBehaviour[] comps;
    private MovementPlayer movement;
    private Rigidbody2D rb;
    private Renderer[] rs;
    public bool interacting = false;
    private PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine && PhotonNetwork.IsConnected)
            this.enabled = false;
        comps = GetComponents<MonoBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        rs = GetComponentsInChildren<Renderer>();
        cam = playerCamera.GetComponent<CameraFollowPlayer>();
        movement = GetComponent<MovementPlayer>();
    }

    void Update()
    {
        if (interactObject)
        {
            if (Input.GetButtonDown("Jump") && !interacting)
            {
                if (interactObject.occupied)
                    return;
                interactObject.UsePost(gameObject.transform.parent.gameObject);
                movement.PlayIdle();
                SetView(false);
                rb.velocity = new Vector2(0, 0);
                interacting = true;
            }
            if (Input.GetButtonDown("Cancel") && interacting)
            {
                // foreach (MonoBehaviour c in comps)
                //     c.enabled = true;
                SetView(true);
                interactObject.GetOutPost();
                interacting = false;
            }
        }
    }

    public void SetView(bool view)
    {
        // foreach (MonoBehaviour c in comps)
        //     c.enabled = view;
        movement.enabled = view;
        this.enabled = true;
        foreach (Renderer r in rs)
            r.enabled = view;
        cam.SwitchCamera();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Post"))
        {
            Post post = collision.gameObject.GetComponent<Post>();
            interactObject = post;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!interacting)
            interactObject = null;
    }
}
