using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoot : MonoBehaviour
{
    void Start()
    {
        transform.parent = GameObject.Find("Spaceship").transform;
    }
}
