using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject shipCamera;
    private Post interactObject;
    private KeyCode interactKey = KeyCode.E;
    private KeyCode cancelKey = KeyCode.R;
    private MonoBehaviour[] comps;
    private Rigidbody2D rb;
    private Renderer[] rs;

    void Start()
    {
        comps = GetComponents<MonoBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        rs = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (interactObject)
        {
            if (Input.GetButtonDown("Jump") && interactObject)
            {
                print("interacting with " + interactObject.name);
                interactObject.UsePost(gameObject);
                foreach (MonoBehaviour c in comps)
                {
                    c.enabled = false;
                }
                this.enabled = true;
                rb.velocity = new Vector2(0, 0);
                SetView(false);
                playerCamera.SetActive(false);
                shipCamera.SetActive(true);
                // gameObject.SetActive(false);
                // gameObject.enabled = false;
            }
            if (Input.GetKeyDown(cancelKey) && interactObject)
            {
                foreach (MonoBehaviour c in comps)
                {
                    c.enabled = true;
                }
                interactObject.GetOutPost();
                SetView(true);
                playerCamera.SetActive(true);
                shipCamera.SetActive(false);
            }
        }
    }

    public void SetView(bool view)
    {
        foreach (Renderer r in rs)
            r.enabled = view;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Post")
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
