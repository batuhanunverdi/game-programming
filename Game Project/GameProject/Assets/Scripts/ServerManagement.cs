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
    public PhotonView playerPrefabFemale;
    private HealthBar healthBar;
    private GameOverScreen gameOverScreen;
    private CinemachineVirtualCamera followCamera;
    private GameObject playerCameraRoot;
    private Image image;
    private LoadScreen loadScreen;
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
        followCamera = PhotonView.Find(5).GetComponent<CinemachineVirtualCamera>();
        image = PhotonView.Find(1).GetComponent<Image>();
        loadScreen = PhotonView.Find(17).GetComponent<LoadScreen>();

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
        GameObject gameObject;
        string s = "PlayerArmature(Clone)/";
        if(PFLogin.prefabName=="Female"){
             gameObject = PhotonNetwork.Instantiate(playerPrefabFemale.name, new Vector3(73, 22, 34), Quaternion.identity, 0,null);
             s = "PlayerArmatureF(Clone)/";
             s += "FemaleCharacterPolyart/";
        }else{
            gameObject = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(73, 22, 34), Quaternion.identity, 0,null);
            s += "MaleCharacterPolyart/";
        }
        GameObject body = GameObject.Find(s+PFLogin.body);
        GameObject cloak = GameObject.Find(s+PFLogin.cloak);
        GameObject shield = GameObject.Find(s+"root/pelvis/spine_01/spine_02/spine_03/clavicle_l/upperarm_l/lowerarm_l/hand_l/weapon_l/"+PFLogin.shield);
        GameObject weapon = GameObject.Find(s+"root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r/"+PFLogin.weapon);
        body.SetActive(true);
        cloak.SetActive(true);
        shield.SetActive(true);
        weapon.SetActive(true);
        gameObject.GetComponent<ThirdPersonController>().enabled = true;
        PlayerAttack pa = gameObject.GetComponent<PlayerAttack>();
        pa.healthBar = healthBar;
        pa.GameOverScreen = gameOverScreen;
        gameObject.transform.position = new Vector3(73, 22, 34);
        pa.w = image;
        playerCameraRoot = GameObject.FindGameObjectWithTag("Player");
        followCamera.Follow = playerCameraRoot.transform;
        CallAfterDelay.Create(1.0f, loadScreen.Setup2);
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