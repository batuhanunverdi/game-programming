using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public CanvasRenderer Skill;
    public Slider Healthbar;
    // Start is called before the first frame update
     public AudioMixer audioMixer;
    public void SetVolume(float volume){
        audioMixer.SetFloat("volume",Mathf.Log10(volume) * 20);
    }
    public void QuitGame(){
        
        Application.Quit();
    }
    public void CloseButton(){
        Skill.gameObject.SetActive(true);
        Healthbar.gameObject.SetActive(true);
    }
    public void SendData()
    {
        var request =
            new UpdateUserDataRequest {
                Data =
                    new Dictionary<string, string> {
                        { "Level", PFLogin.level },
                        { "Exp", PFLogin.exp },
                        { "Gold", PFLogin.gold}
                    }
            };
        PlayFabClientAPI.UpdateUserData (request, OnDataSend, OnError);
    }
    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Succesful!");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}