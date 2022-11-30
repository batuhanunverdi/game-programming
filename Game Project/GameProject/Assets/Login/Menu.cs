using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   bool loggedIn = true;
   //void Start(){
   // if(loggedIn){
   //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   // }
   //}
    public void LoginButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RegisterButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame(){
        
        Application.Quit();
    }

}
