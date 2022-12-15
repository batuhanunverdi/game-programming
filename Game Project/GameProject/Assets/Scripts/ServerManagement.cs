using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using Cinemachine;
using StarterAssets;
using System;

public class ServerManagement : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab1_0;
    public PhotonView playerPrefab1_1;
    public PhotonView playerPrefab1_2;
    public PhotonView playerPrefab2_0;
    public PhotonView playerPrefab2_1;
    public PhotonView playerPrefab2_2;
    public PhotonView playerPrefab3_0;
    public PhotonView playerPrefab3_1;
    public PhotonView playerPrefab3_2;
    public PhotonView playerPrefab4_0;
    public PhotonView playerPrefab4_1;
    public PhotonView playerPrefab4_2;
    public PhotonView playerPrefabFemale1_0;
    public PhotonView playerPrefabFemale1_1;
    public PhotonView playerPrefabFemale1_2;
    public PhotonView playerPrefabFemale2_0;
    public PhotonView playerPrefabFemale2_1;
    public PhotonView playerPrefabFemale2_2;
    public PhotonView playerPrefabFemale3_0;
    public PhotonView playerPrefabFemale3_1;
    public PhotonView playerPrefabFemale3_2;
    public PhotonView playerPrefabFemale4_0;
    public PhotonView playerPrefabFemale4_1;
    public PhotonView playerPrefabFemale4_2;
    private HealthBar healthBar;
    private GameOverScreen gameOverScreen;
    private CinemachineVirtualCamera followCamera;
    private GameObject playerCameraRoot;
    private Image image;
    private LoadScreen loadScreen;
    private ExpBar expBar;
    private TextMesh level;
    PhotonView[] array;
    PhotonView temp;
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
        expBar = PhotonView.Find(21).GetComponent<ExpBar>();
        //level = PhotonView.Find(22).GetComponent<TextMesh>();
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
        if(PFLogin.gender == "Female"){
            if(PFLogin.shield == "Shield0"){
                if(PFLogin.weapon == "Sword0") PFLogin.prefabName = "PlayerArmatureF.1.0";
                else if (PFLogin.weapon == "Sword04.1") PFLogin.prefabName = "PlayerArmatureF.1.1";
                else if (PFLogin.weapon == "Sword03.1") PFLogin.prefabName = "PlayerArmatureF.1.2";
            }else if(PFLogin.shield == "Shield1.5"){
                if(PFLogin.weapon == "Sword0") PFLogin.prefabName = "PlayerArmatureF.2.0";
                else if (PFLogin.weapon == "Sword04.1") PFLogin.prefabName = "PlayerArmatureF.2.1";
                else if (PFLogin.weapon == "Sword03.1") PFLogin.prefabName = "PlayerArmatureF.2.2";
            }else if(PFLogin.shield == "Shield1"){
                if(PFLogin.weapon == "Sword0") PFLogin.prefabName = "PlayerArmatureF.3.0";
                else if (PFLogin.weapon == "Sword04.1") PFLogin.prefabName = "PlayerArmatureF.3.1";
                else if (PFLogin.weapon == "Sword03.1") PFLogin.prefabName = "PlayerArmatureF.3.2";
            }else if(PFLogin.shield == "Shield0.6"){
                if(PFLogin.weapon == "Sword0") PFLogin.prefabName = "PlayerArmatureF.4.0";
                else if (PFLogin.weapon == "Sword04.1") PFLogin.prefabName = "PlayerArmatureF.4.1";
                else if (PFLogin.weapon == "Sword03.1") PFLogin.prefabName = "PlayerArmatureF.4.2";
            }
        }else{
            if(PFLogin.shield == "Shield0"){
                if(PFLogin.weapon == "Sword0") PFLogin.prefabName = "PlayerArmature.1.0";
                else if (PFLogin.weapon == "Sword04.1") PFLogin.prefabName = "PlayerArmature.1.1";
                else if (PFLogin.weapon == "Sword03.1") PFLogin.prefabName = "PlayerArmature.1.2";
            }else if(PFLogin.shield == "Shield1.5"){
                if(PFLogin.weapon == "Sword0") PFLogin.prefabName = "PlayerArmature.2.0";
                else if (PFLogin.weapon == "Sword04.1") PFLogin.prefabName = "PlayerArmature.2.1";
                else if (PFLogin.weapon == "Sword03.1") PFLogin.prefabName = "PlayerArmature.2.2";
            }else if(PFLogin.shield == "Shield1"){
                if(PFLogin.weapon == "Sword0") PFLogin.prefabName = "PlayerArmature.3.0";
                else if (PFLogin.weapon == "Sword04.1") PFLogin.prefabName = "PlayerArmature.3.1";
                else if (PFLogin.weapon == "Sword03.1") PFLogin.prefabName = "PlayerArmature.3.2";
            }else if(PFLogin.shield == "Shield0.6"){
                if(PFLogin.weapon == "Sword0") PFLogin.prefabName = "PlayerArmature.4.0";
                else if (PFLogin.weapon == "Sword04.1") PFLogin.prefabName = "PlayerArmature.4.1";
                else if (PFLogin.weapon == "Sword03.1") PFLogin.prefabName = "PlayerArmature.4.2";
            }
        }
        if(PFLogin.gender=="Male"){
            array = new PhotonView[] {playerPrefab1_0,playerPrefab1_1,playerPrefab1_2,playerPrefab2_0,playerPrefab2_1,playerPrefab2_2,playerPrefab3_0,
            playerPrefab3_1,playerPrefab3_2,playerPrefab4_0,playerPrefab4_1,playerPrefab4_2};
        }else{
            array = new PhotonView[] {playerPrefabFemale1_0,playerPrefabFemale1_1,playerPrefabFemale1_2,playerPrefabFemale2_0,playerPrefabFemale2_1,playerPrefabFemale2_2,playerPrefabFemale3_0,
            playerPrefabFemale3_1,playerPrefabFemale3_2,playerPrefabFemale4_0,playerPrefabFemale4_1,playerPrefabFemale4_2};
        }
        Debug.Log(PFLogin.prefabName);
        Debug.Log(PFLogin.gender);
        for(int i=0;i<array.Length;i++){
            temp = array[i];
            if(temp.name == PFLogin.prefabName) break;
        }
        GameObject gameObject;
        gameObject = PhotonNetwork.Instantiate(temp.name, new Vector3(73, 22, 34), Quaternion.identity, 0,null);
        gameObject.GetComponent<ThirdPersonController>().enabled = true;
        PlayerAttack pa = gameObject.GetComponent<PlayerAttack>();
        pa.healthBar = healthBar;
        pa.GameOverScreen = gameOverScreen;
        gameObject.transform.position = new Vector3(73, 22, 34);
        pa.w = image;
        pa.level = Convert.ToInt32(PFLogin.level);
        pa.exp = Convert.ToInt32(PFLogin.exp);
        //pa.levelText = level;
        pa.expBar = expBar;
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