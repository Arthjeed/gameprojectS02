using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    // public GameObject playerCamera;
    public GameObject shipCamera;
    public GameObject playerCamera;
    private CameraFollowPlayer cam;
    private Post interactObject;
    private KeyCode cancelKey = KeyCode.R;
    private MonoBehaviour[] comps;
    private MovementPlayer movement;
    private Rigidbody2D rb;
    private Renderer[] rs;
    private bool interacting = false;

    void Start()
    {
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
                print("interacting with " + interactObject.name);
                interactObject.UsePost(gameObject);
                movement.PlayIdle();
                SetView(false);
                rb.velocity = new Vector2(0, 0);
                interacting = true;
            }
            if (Input.GetButtonDown("Cancel") && interacting)
            {
                foreach (MonoBehaviour c in comps)
                    c.enabled = true;
                SetView(true);
                interactObject.GetOutPost();
                interacting = false;
            }
        }
    }

    public void SetView(bool view)
    {
        foreach (MonoBehaviour c in comps)
            c.enabled = view;
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
        interactObject = null;
    }
}
