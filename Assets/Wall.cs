using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");
        print(collision.gameObject.tag);
    }
}
