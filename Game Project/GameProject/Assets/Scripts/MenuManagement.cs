using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManagement : MonoBehaviour
{
    public CanvasRenderer Skill;
    public Slider Healthbar;
    public CanvasRenderer EscMenu;
    public TMP_Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = PFLogin.name;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(EscMenu.gameObject.active){
                EscMenu.gameObject.SetActive(false);
                Skill.gameObject.SetActive(true);
                Healthbar.gameObject.SetActive(true);
            }else{
                Healthbar.gameObject.SetActive(false);
                Skill.gameObject.SetActive(false);
                EscMenu.gameObject.SetActive(true);
            }
        }
    }
}
