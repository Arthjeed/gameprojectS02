using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiplayerSceneIndex;

    [SerializeField]
    private GameObject lobbyPanel;
    [SerializeField]
    private GameObject roomPanel;

    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private Transform playerContainer;

    [SerializeField]
    private GameObject playerListingPrefab;
    [SerializeField]
    private Text roomNameDisplay;

    void clearPlayerListing(){
        for (int i = playerContainer.childCount - 1 ; i >= 0 ; i--)
        {
            Destroy(playerContainer.GetChild(i).gameObject);
        }
    }

    void ListPlayers() {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject tempListing = Instantiate(playerListingPrefab, playerContainer);
            Text tempText = tempListing.transform.GetChild(0).GetComponent<Text>();
            tempText.text = player.NickName;
        }
    }

    public override void OnJoinedRoom() {
        Debug.Log("Joined Room");
        roomPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        roomNameDisplay.text = PhotonNetwork.CurrentRoom.Name;
        startButton.SetActive(PhotonNetwork.IsMasterClient);
        clearPlayerListing();
        ListPlayers();

    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        clearPlayerListing();
        ListPlayers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        clearPlayerListing();
        ListPlayers();
        if (PhotonNetwork.IsMasterClient) {
            startButton.SetActive(true);
        }
    }

    public void startGame() {
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }

    IEnumerator rejoinLobby() {
        yield return new WaitForSeconds(2);
        PhotonNetwork.JoinLobby();
    }

    public void BackOnClick() {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(rejoinLobby());
    }

    public override void OnEnable() {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable() {
        PhotonNetwork.RemoveCallbackTarget(this);
    }


    private void StartGame() {
        if(PhotonNetwork.IsMasterClient) {
            Debug.Log("starting game");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
