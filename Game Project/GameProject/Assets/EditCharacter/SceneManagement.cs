using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(flag == true){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
       
    }
    void OnDataSend(UpdateUserDataResult result)
    {
        flag = true;
        Debug.Log("Succesful!");
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Something went wrong, please try again");
    }
}
