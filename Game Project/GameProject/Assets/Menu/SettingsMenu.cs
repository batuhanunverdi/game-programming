using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class SettingsMenu : MonoBehaviour
{
    bool flag = false;
    public CanvasRenderer Skill;
    public Slider Healthbar;
    // Start is called before the first frame update
     public AudioMixer audioMixer;
    public void SetVolume(float volume){
        audioMixer.SetFloat("volume",Mathf.Log10(volume) * 20);
    }
    public void QuitGame(){
        SendData();
        if(flag == true) Application.Quit();
        else {
            Debug.Log("Something went wrong, please try again.");
        }
    }
    public void CloseButton(){
        Skill.gameObject.SetActive(true);
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
        flag = true;
        Debug.Log("Succesful!");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    public void EditCharacter(){
        PFLogin.shopOrEdit="Edit";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Shop(){
        PFLogin.shopOrEdit="Shop";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
