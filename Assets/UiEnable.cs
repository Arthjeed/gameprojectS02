using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UiEnable : MonoBehaviour
{
    public GameObject UI;
    private PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Enable()
    {
        // if (PV.IsMine && col.gameObject.CompareTag("Player"))
            UI.SetActive(true);
    }

    public void Disable()
    {
        // if (PV.IsMine && col.gameObject.CompareTag("Player"))
            UI.SetActive(false);
    }
}
