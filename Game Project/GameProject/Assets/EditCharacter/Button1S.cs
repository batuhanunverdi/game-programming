using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button1S : MonoBehaviour
{
    public Button button ;
    // Start is called before the first frame update
    void Start()
    {
        if(!PFLogin.weaponInventory.Contains("Sword04.1,")) {
            button.interactable = true;
            GameObject text = GameObject.Find("Canvas/Shop/Shop/Outer/Inner/"+button.name+"/Price");
            GameObject text2 = GameObject.Find("Canvas/Shop/Shop/Outer/Inner/"+button.name+"/Status");
            text2.SetActive(false);
            text.SetActive(true);
        }
        if(int.Parse(PFLogin.gold) < 30){
             button.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick(){
        Statics.Temp = "30";
        GameObject.Find(Statics.PrefabName +"clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r/"+Statics.weapon).SetActive(false);        
        GameObject.Find(Statics.PrefabName +"clavicle_l/upperarm_l/lowerarm_l/hand_l/weapon_l/"+Statics.shield).SetActive(false);        
        GameObject.Find(Statics.PrefabName +"clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r/Sword04.1").SetActive(true);
        Statics.weapon = "Sword04.1";
    }
}
