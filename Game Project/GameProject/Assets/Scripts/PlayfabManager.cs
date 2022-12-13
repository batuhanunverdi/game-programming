using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    bool flag = false;
    [Header("UI")]
    public TMP_Text messageText;

    public TMP_InputField emailInput;

    public TMP_InputField passwordInput;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void RegisterButton()
    {
        messageText.text = "";
        if (passwordInput.text.Length < 6)
        {
            messageText.text = "Password too short!";
            return;
        }
        var request =
            new RegisterPlayFabUserRequest {
                Email = emailInput.text,
                Password = passwordInput.text,
                RequireBothUsernameAndEmail = false
            };

        PlayFabClientAPI.RegisterPlayFabUser (
            request,
            OnRegisterSuccess,
            OnError
        );
    }

    public void LoginButton()
    {
        messageText.text = "";
        if (passwordInput.text.Length < 6)
        {
            messageText.text = "Password too short!";
            return;
        }
        var request =
            new LoginWithEmailAddressRequest {
                Email = emailInput.text,
                Password = passwordInput.text
            };
        PlayFabClientAPI.LoginWithEmailAddress (
            request,
            OnLoginSuccess,
            OnError
        );
        //PFLogin.email = request.Email;
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Registered and logged in!");
        UserDisplayName();
        CreateData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public static string RandomString()
    {
       const string glyphs= "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; 
       int charAmount = 7;
       string myString = "";
       for(int i=0; i<charAmount; i++)
        {
             myString += glyphs[Random.Range(0, glyphs.Length)];
        }
        return myString;
    }
    public void CreateData()
    {
        var request =
            new UpdateUserDataRequest {
                Data =
                    new Dictionary<string, string> {
                        { "Level", "1" },
                        { "Exp", "0" },
                        { "Gold", "0"},
                        { "Character", "Female" },
                        { "Cloak", "Cloak03"},
                        { "Body", "body10"},
                        { "Weapon", "Sword0"},
                        { "Shield", "Shield1"}
                    }
            };
        PlayFabClientAPI.UpdateUserData (request, OnDataSend, OnError);

    }
    void UserDisplayName() {
        float randomNumber = Random.Range(0, 10000000);
        string randomString = RandomString();
        PlayFabClientAPI.UpdateUserTitleDisplayName( new UpdateUserTitleDisplayNameRequest {
            DisplayName = "Lazy"+randomNumber+randomString
        }, result => {
            Debug.Log("The player's display name is now: " + result.DisplayName);
            flag = true;
        }, error =>{
            Debug.LogError(error.GenerateErrorReport());
            flag = false;
        } );
    }
    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Succesful!");
    }

    void OnError(PlayFabError error)
    {
        if (
            error.GenerateErrorReport() ==
            "/Client/RegisterPlayFabUser: Email address not available\nEmail: Email address already exists. "
        )
        {
            messageText.text = "Email address already exists!";
        }
        else if (
            error.GenerateErrorReport() ==
            "/Client/RegisterPlayFabUser: Invalid input parameters\nEmail: Email address is not valid."
        )
        {
            messageText.text = "Invalid email!";
        }
        else if (
            error.GenerateErrorReport() ==
            "/Client/LoginWithEmailAddress: User not found"
        )
        {
            messageText.text = "No such user! Please register first.";
        }
        else if (
            error.GenerateErrorReport() ==
            "/Client/LoginWithEmailAddress: Invalid input parameters\nEmail: Email address is not valid."
        )
        {
            messageText.text = "Invalid email!";
        }
        else if (
            error.GenerateErrorReport() ==
            "/Client/LoginWithEmailAddress: Invalid email address or password"
        )
        {
            messageText.text = "Wrong password!";
        }
        else
        {
            messageText.text = "Unknown error";
        }
        Debug.Log(error.GenerateErrorReport());
    }

    // Update is called once per frame
    public void Quit()
    {
        messageText.text = "";
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
