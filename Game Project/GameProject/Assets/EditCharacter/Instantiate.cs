using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PFLogin.gender == "Female"){
            Statics.PrefabName = "Canvas/Design/Prefabs/PlayerArmatureFemale";
        }else{
            Statics.PrefabName = "Canvas/Design/Prefabs/PlayerArmature";
        }
        GameObject.Find(Statics.PrefabName).SetActive(true);
        if(PFLogin.gender == "Female"){
            Statics.PrefabName += "/FemaleCharacterPolyart";
        }else{
            Statics.PrefabName += "/MaleCharacterPolyart";
        }
        Statics.PrefabName += "/root/pelvis/spine_01/spine_02/spine_03/";
        GameObject.Find(Statics.PrefabName +"clavicle_l/upperarm_l/lowerarm_l/hand_l/weapon_l/"+PFLogin.shield).SetActive(true);
        GameObject.Find(Statics.PrefabName +"clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r/"+PFLogin.weapon).SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
