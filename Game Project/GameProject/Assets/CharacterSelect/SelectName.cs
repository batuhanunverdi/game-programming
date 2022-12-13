using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectName : MonoBehaviour
{
    bool flag = false;
    public TMP_InputField name;
    public TMP_Text errorText;

    public Transform parentObject;

    public GameObject Female;

    public GameObject Male;

    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        if (Temp.character == "Female")
        {
            go = Instantiate(Female, parentObject);
        }
        else if (Temp.character == "Male")
        {
            go = Instantiate(Male, parentObject);
        }
        go.transform.localScale = new Vector3(220, 220, 220);
        go.transform.Rotate(0f, -180f, 0f);
    }

    public void BackButton()
    {
        Destroy (go);
    }

    public void FinishButton()
    {
        if(name.text.Length > 3 && name.text.Length<14){
            UpdateDisplayName();
            if(flag) CreateData();
        }else if(name.text.Length == 3){
            errorText.text = "Name too short!";
            return;
        }else {
            errorText.text = "Name too long!";
            return;
        }
    }

    public void CreateData()
    {
        string cloak = "Cloak02";
        string weapon = "Sword00";
        string shield = "Shield0";
        if(Temp.character == "Female"){
            cloak = "Cloak03";
            weapon = "Sword0";
            shield = "Shield1";
        }
        var request =
            new UpdateUserDataRequest {
                Data =
                    new Dictionary<string, string> {
                        { "Level", "1" },
                        { "Exp", "0" },
                        { "Gold", "0"},
                        { "Character", Temp.character },
                        { "Body", "Body10"},
                        { "Cloak", cloak},
                        { "Weapon", weapon},
                        { "Shield", shield}
                    }
            };
        PlayFabClientAPI.UpdateUserData (request, OnDataSend, OnError);
    }
    void UpdateDisplayName() {
        PlayFabClientAPI.UpdateUserTitleDisplayName( new UpdateUserTitleDisplayNameRequest {
            DisplayName = name.text
        }, result => {
            Debug.Log("The player's display name is now: " + result.DisplayName);
            flag = true;
        }, error => {Debug.LogError(error.GenerateErrorReport());
            errorText.text = "Username not available!";
            flag = false;
        }
        
        );
    }
    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Succesful!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    void Update()
    {
        if (go == null)
        {
            Start();
        }
    }
}
