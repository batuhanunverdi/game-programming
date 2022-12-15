using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button4 : MonoBehaviour
{
    public Button button ;
    // Start is called before the first frame update
    void Start()
    {
        if(PFLogin.shieldInventory.Contains("Shield1,")) button.interactable = true;
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
        if(Statics.shield is not null){
            GameObject.Find(Statics.PrefabName +"clavicle_l/upperarm_l/lowerarm_l/hand_l/weapon_l/"+Statics.shield).SetActive(false);
        }
        GameObject.Find(Statics.PrefabName +"clavicle_l/upperarm_l/lowerarm_l/hand_l/weapon_l/Shield1").SetActive(true);
        Statics.shield = "Shield1";
    }
}
