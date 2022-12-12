using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public CanvasRenderer Skill;
    public Slider Healthbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitGame(){
        
        Application.Quit();
    }
    public void CloseButton(){
        Skill.gameObject.SetActive(true);
        Healthbar.gameObject.SetActive(true);
    }
}
