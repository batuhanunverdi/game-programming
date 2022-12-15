using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button5 : MonoBehaviour
{
    public Button button ;
    // Start is called before the first frame update
    void Start()
    {
        if(PFLogin.shieldInventory.Contains("Shield0.6,")) button.interactable = true;
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
        GameObject.Find(Statics.PrefabName +"clavicle_l/upperarm_l/lowerarm_l/hand_l/weapon_l/Shield0.6").SetActive(true);
        Statics.shield = "Shield0.6";
    }
}
