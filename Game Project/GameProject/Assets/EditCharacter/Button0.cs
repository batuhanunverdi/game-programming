using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button0 : MonoBehaviour
{
    public Button button ;
    // Start is called before the first frame update
    void Start()
    {
        if(PFLogin.weaponInventory.Contains("Sword0,")) button.interactable = true;
        else{
            GameObject text = GameObject.Find("Canvas/Items/Outer/Inner/"+button.name+"/Status");
            text.SetActive(true);
        }
    }

    public void OnClick(){
        GameObject.Find(Statics.PrefabName + "clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r/"+Statics.weapon).SetActive(false);
        GameObject.Find(Statics.PrefabName + "clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r/Sword0").SetActive(true);
        Statics.weapon = "Sword0";
    }
}
