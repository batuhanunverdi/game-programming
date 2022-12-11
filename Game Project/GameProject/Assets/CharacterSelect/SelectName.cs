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
    public TMP_InputField name;

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
        if(name.text.Length > 4 && name.text.Length<14){
            CreateData();
        }
    }

    public void CreateData()
    {
        var request =
            new UpdateUserDataRequest {
                Data =
                    new Dictionary<string, string> {
                        { "Character", Temp.character },
                        { "Name", name.text.ToString() },
                        { "Hair", "Default"},
                        { "Weapon", "Default"},
                        { "Shield", "Default"}
                    }
            };
        PlayFabClientAPI.UpdateUserData (request, OnDataSend, OnError);
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
