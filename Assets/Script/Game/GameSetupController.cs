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
    public AudioSource backgroundMusic;
    
    private void OnEnable() {
        if (GameSetupController.GS == null) {
            GameSetupController.GS = this;
        }
    }
    
    void Start()
    {
        CreatePlayer();
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    private void CreatePlayer() {
        if (PhotonNetwork.CountOfPlayers <= 1)
            PhotonNetwork.OfflineMode = true;
        Vector3 playerPos = new Vector3(0, 32.6f, 0);
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("photonPrefabs", "Player"), playerPos, spaceship.rotation);
        player.transform.parent = spaceship;
        player.transform.localPosition = playerPos;
    }
}
