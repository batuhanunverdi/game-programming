using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SceneManagement : MonoBehaviour
{
    bool flag = false;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject quit;
    public GameObject saveAndQuit;
    public GameObject buyQuit;
    public Button buy;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        if(PFLogin.shopOrEdit=="Edit"){
            panel1.SetActive(true);
            saveAndQuit.SetActive(true);
        }else{
            GameObject.Find(Statics.PrefabName +"clavicle_l/upperarm_l/lowerarm_l/hand_l/weapon_l/"+PFLogin.shield).SetActive(false);
            GameObject.Find(Statics.PrefabName +"clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r/"+PFLogin.weapon).SetActive(false);
            panel2.SetActive(true);
            buyQuit.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Statics.Temp is not null) buy.interactable = true;
    }
    public void Quit(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void SaveAndQuit(){
        if(Statics.shield is null){
            Statics.shield = PFLogin.shield;
        }
        if(Statics.weapon is null){
            Statics.weapon = PFLogin.weapon;
        }
        var request =
            new UpdateUserDataRequest {
                Data =
                    new Dictionary<string, string> {
                        { "Weapon", Statics.weapon },
                        { "Shield", Statics.shield }
                    }
            };
        PlayFabClientAPI.UpdateUserData (request, OnDataSend, OnError);
    }
    public void BuyQuit(){
        Debug.Log("i work");
        string s ="";
        int a = int.Parse(PFLogin.gold)-int.Parse(Statics.Temp);
        if(Statics.weapon is not null){
            s += PFLogin.weaponInventory + Statics.weapon+",";
            var request =
            new UpdateUserDataRequest {
                Data =
                    new Dictionary<string, string> {
                        {"WeaponInventory",s},
                        {"Gold",a.ToString()}
                    }
            };
             PlayFabClientAPI.UpdateUserData (request, OnDataSend, OnError);
        }
        if(Statics.shield is not null){
            s += PFLogin.shieldInventory + Statics.shield+",";
            var request =
            new UpdateUserDataRequest {
                Data =
                    new Dictionary<string, string> {
                        {"ShieldInventory",s},
                        {"Gold",a.ToString()}
                    }
            };
             PlayFabClientAPI.UpdateUserData (request, OnDataSend, OnError);
        }
    }
    void OnDataSend(UpdateUserDataResult result)
    {
        Statics.weapon=null;
        Statics.shield=null;
        Statics.Temp=null;
        Debug.Log("Succesful!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Something went wrong, please try again");
    }
}
