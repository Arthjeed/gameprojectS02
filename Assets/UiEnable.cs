using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UiEnable : MonoBehaviour
{
    public GameObject UI;
    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(false);
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (PV.IsMine)
            UI.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (PV.IsMine)
            UI.SetActive(false);
    }
}
