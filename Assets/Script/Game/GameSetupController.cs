using System.Collections;
using Photon.Pun;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    public static GameSetupController GS;

    public Transform[] spawnPoints;
    // Start is called before the first frame update
    
    private void OnEnable() {
        if (GameSetupController.GS == null) {
            GameSetupController.GS = this;
        }
    }
    
    void Start()
    {

        CreatePlayer();
    }

    private void CreatePlayer() {
        Debug.Log("creating Player");
        PhotonNetwork.Instantiate(Path.Combine("photonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }
}
