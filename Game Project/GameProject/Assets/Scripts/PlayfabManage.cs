using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;

public class PlayfabManage : MonoBehaviour
{
   void Start(){
      //getName();
   }
   
    /* public void getName(){
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
     }
     void OnDataReceived(GetUserDataResult result){
        Debug.Log("Okey");
        if(result.Data !=null && result.Data.ContainsKey("Name")){
            Debug.Log(result.Data["Name"].Value);
        }
     }
     void OnError(PlayFabError error){
        Debug.Log(error.GenerateErrorReport());
     }
     */
 
}
