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
    public Transform spaceship;
    
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
        Vector3 playerPos = new Vector3(0, 32.6f, 0);
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("photonPrefabs", "Player"), playerPos, spaceship.rotation);
        player.transform.parent = spaceship;
        player.transform.localPosition = playerPos;
    }
}
