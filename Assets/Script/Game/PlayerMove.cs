using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D rigidBody2D;

    public Transform corner_max;
    public Transform corner_min;

    private float x_min;
    private float y_min;
    private float x_max;
    private float y_max;
    private Animator anim;
    private bool animationIsPlaying = false;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        x_max = corner_max.position.x;
        x_min = corner_min.position.x;
        y_max = corner_max.position.y;
        y_min = corner_min.position.y;
        anim = GetComponent<Animator>();
    }

    private IEnumerator PlayAnimation(string animName)
    {
        anim.Play(animName, 0);
        yield return new WaitForEndOfFrame();
        animationIsPlaying = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        animationIsPlaying = false;
    }
    private void KeepWithinMinMaxRectangle()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        float clampedX = Mathf.Clamp(x, x_min, x_max);
        float clampedY = Mathf.Clamp(y, y_min, y_max);
        transform.position = new Vector3(clampedX, clampedY, z);
    }


    void FixedUpdate()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        if (xMove != 0 || yMove != 0)
            StartCoroutine(PlayAnimation("Run"));
        float xSpeed = xMove * speed;
        float ySpeed = yMove * speed;
        Vector2 newVelocity = new Vector2(xSpeed, ySpeed);
        rigidBody2D.velocity = newVelocity;
        // restrict player movement
        // KeepWithinMinMaxRectangle();

    }
    void OnDrawGizmos()
    {
        Vector3 top_right = Vector3.zero; Vector3 bottom_right = Vector3.zero;
        Vector3 bottom_left = Vector3.zero;
        Vector3 top_left = Vector3.zero;
        if (corner_max && corner_min)
        {
            top_right = corner_max.position;
            bottom_left = corner_min.position;
            bottom_right = top_right;
            bottom_right.y = bottom_left.y;
            top_left = top_right;
            top_left.x = bottom_left.x;
        }
        //Set the following gizmo colors to YELLOW
        Gizmos.color = Color.yellow;
        //Draw 4 lines making a rectangle
        Gizmos.DrawLine(top_right, bottom_right);
        Gizmos.DrawLine(bottom_right, bottom_left);
        Gizmos.DrawLine(bottom_left, top_left);
        Gizmos.DrawLine(top_left, top_right);
    }
}
