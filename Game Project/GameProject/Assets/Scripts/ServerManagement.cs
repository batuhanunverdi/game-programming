using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using Cinemachine;
using StarterAssets;

public class ServerManagement : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    private HealthBar healthBar;
    private GameOverScreen gameOverScreen;
    private TeleportDesert hellCube;
    private CinemachineVirtualCamera followCamera;
    private GameObject playerCameraRoot;
    private Image image;
    // Start is called before the first frame update

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        healthBar = PhotonView.Find(3).GetComponent<HealthBar>();
        gameOverScreen = PhotonView.Find(2).GetComponent<GameOverScreen>();
        hellCube = PhotonView.Find(4).GetComponent<TeleportDesert>();
        followCamera = PhotonView.Find(5).GetComponent<CinemachineVirtualCamera>();
        image = PhotonView.Find(1).GetComponent<Image>();
    }



    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the server");
        PhotonNetwork.JoinLobby();
        // connect to lobby
        //It checks/controls the connection of the server

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to the lobby");
        PhotonNetwork.JoinOrCreateRoom("AkdenizCSRoom", new RoomOptions { MaxPlayers = 4, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        //connect to random room or create 
        //it checks the connection of the Lobby
    }

    public override void OnJoinedRoom()
    {
        GameObject gameObject = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(73, 22, 34), Quaternion.identity, 0,null);
        gameObject.GetComponent<ThirdPersonController>().enabled = true;
        PlayerAttack pa = gameObject.GetComponent<PlayerAttack>();
        pa.healthBar = healthBar;
        pa.GameOverScreen = gameOverScreen;
        gameObject.transform.position = new Vector3(73, 22, 34);
        pa.w = image;
        playerCameraRoot = GameObject.FindGameObjectWithTag("Player");
        followCamera.Follow = playerCameraRoot.transform;
        pa.GameOverScreen.Setup2();
        Debug.Log("Connected to the Room");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left the Room");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Left the Lobby");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Could not join any room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Could not join any random room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Could not create room");
    }


}