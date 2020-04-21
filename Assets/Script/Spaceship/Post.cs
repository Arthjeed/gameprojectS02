using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post : MonoBehaviour
{
    public GameObject post;
    private GameObject postPlayer;

    public enum postType
    {
        Canon, Thruster, Shield
    };

    public Post.postType type;

    void Start()
    {

    }

    void Update()
    {

    }

    public void GetOutPost() {
        if (postPlayer)
        {
            // ThrusterControls tmp = post.GetComponent<ThrusterControls>();
            // if (tmp)
            //     tmp.enabled = false;
            // else
            // {
            //     Canon tmpCanon = post.GetComponent<Canon>();
            //     tmpCanon.enabled = false;
            // }
            postPlayer = null;
            switch (type)
            {
                case postType.Canon:
                    Canon tmpCanon = post.GetComponent<Canon>();
                    tmpCanon.enabled = false;
                    break;
                case postType.Thruster:
                    ThrusterControls tmp = post.GetComponent<ThrusterControls>();
                    tmp.enabled = false;
                    break;
                default:
                    break;
            }
        }
    }

    public void UsePost(GameObject player)
    {
        if (!postPlayer)
        {
            postPlayer = player;
            // ThrusterControls tmp = post.GetComponent<ThrusterControls>();
            // if (tmp)
            //     tmp.enabled = true;
            // else
            // {
            //     Canon tmpCanon = post.GetComponent<Canon>();
            //     tmpCanon.enabled = true;
            // }
            switch (type)
            {
                case postType.Canon:
                    Canon tmpCanon = post.GetComponent<Canon>();
                    tmpCanon.enabled = true;
                    break;
                case postType.Thruster:
                    ThrusterControls tmp = post.GetComponent<ThrusterControls>();
                    tmp.enabled = true;
                    break;
                default:
                    break;
            }
        }
    }

}
