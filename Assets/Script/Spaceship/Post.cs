using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post : MonoBehaviour
{
    public GameObject post;
    private GameObject postPlayer;

    public enum postType
    {
        Canon, Turret, Thruster, Shield, Rotate, MiniShip
    };

    public Post.postType type;

    void Start()
    {

    }

    public void GetOutPost() {
        if (postPlayer)
        {
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
                case postType.Turret:
                    Turret turret = post.GetComponent<Turret>();
                    turret.enabled = false;
                    break;
                case postType.Rotate:
                    RotateShip rotate = post.GetComponent<RotateShip>();
                    rotate.enabled = false;
                    break;
                case postType.Shield:
                    Shield shield = post.GetComponent<Shield>();
                    shield.enabled = false;
                    break;
                case postType.MiniShip:
                    MiniShip ship = post.GetComponent<MiniShip>();
                    ship.enabled = false;
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
                case postType.Turret:
                    Turret turret = post.GetComponent<Turret>();
                    turret.enabled = true;
                    break;
                case postType.Rotate:
                    RotateShip rotate = post.GetComponent<RotateShip>();
                    rotate.enabled = true;
                    break;
                case postType.Shield:
                    Shield shield = post.GetComponent<Shield>();
                    shield.enabled = true;
                    break;
                case postType.MiniShip:
                    MiniShip ship = post.GetComponent<MiniShip>();
                    ship.ActivateShip();
                    ship.enabled = true;
                    break;
                default:
                    break;
            }
        }
    }

}
