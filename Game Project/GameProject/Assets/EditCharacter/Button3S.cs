using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button3S : MonoBehaviour
{
    public Button button ;
    // Start is called before the first frame update
    void Start()
    {
        if(!PFLogin.shieldInventory.Contains("Shield0,")) {
            button.interactable = true;
            GameObject text = GameObject.Find("Canvas/Shop/Shop/Outer/Inner/"+button.name+"/Price");
            GameObject text2 = GameObject.Find("Canvas/Shop/Shop/Outer/Inner/"+button.name+"/Status");
            text2.SetActive(false);
            text.SetActive(true);
        }
        if(int.Parse(PFLogin.gold) < 20){
             button.interactable = false;
        }
    }

    public void OnClick(){
        Statics.buyShield = true;
        Statics.Temp = "20";
        GameObject.Find(Statics.PrefabName +"clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r/"+Statics.weapon).SetActive(false);
        GameObject.Find(Statics.PrefabName +"clavicle_l/upperarm_l/lowerarm_l/hand_l/weapon_l/"+Statics.shield).SetActive(false);
        GameObject.Find(Statics.PrefabName +"clavicle_l/upperarm_l/lowerarm_l/hand_l/weapon_l/Shield0").SetActive(true);
        Statics.shield = "Shield0";
    }
}
