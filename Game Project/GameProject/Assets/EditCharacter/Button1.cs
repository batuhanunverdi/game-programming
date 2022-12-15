using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button1 : MonoBehaviour
{
    public Button button ;
    // Start is called before the first frame update
    void Start()
    {
        if(PFLogin.weaponInventory.Contains("Sword04.1,")) button.interactable = true;
        else{
            GameObject text = GameObject.Find("Canvas/Items/Outer/Inner/"+button.name+"/Status");
            text.SetActive(true);
        } 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick(){
        GameObject.Find(Statics.PrefabName +"clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r/"+Statics.weapon).SetActive(false);
        GameObject.Find(Statics.PrefabName +"clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r/Sword04.1").SetActive(true);
        Statics.weapon = "Sword04.1";
    }
}
