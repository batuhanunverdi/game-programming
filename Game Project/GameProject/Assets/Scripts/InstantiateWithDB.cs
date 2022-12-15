using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;

public class InstantiateWithDB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetData();
    }
    public void GetData()
    {
    
        PlayFabClientAPI
            .GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    void OnDataReceived(GetUserDataResult result)
    {
        if (
            result.Data != null &&
            result.Data.ContainsKey("Level") &&
            result.Data.ContainsKey("Exp") && 
            result.Data.ContainsKey("Gold")&& 
            result.Data.ContainsKey("Weapon")&& 
            result.Data.ContainsKey("Shield")&& 
            result.Data.ContainsKey("Character") && 
            result.Data.ContainsKey("ShieldInventory") &&
            result.Data.ContainsKey("WeaponInventory")
        )
        {
            
            PFLogin.level = result.Data["Level"].Value;
            PFLogin.exp = result.Data["Exp"].Value;
            PFLogin.gold = result.Data["Gold"].Value;
            PFLogin.weapon = result.Data["Weapon"].Value;
            PFLogin.shield = result.Data["Shield"].Value;
            PFLogin.shieldInventory = result.Data["ShieldInventory"].Value;
            PFLogin.weaponInventory = result.Data["WeaponInventory"].Value;
            if(result.Data["Character"].Value == "Female" ){
                PFLogin.gender = "Female";
            }else{
                PFLogin.gender = "Male";
            }
        }
    }
    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
