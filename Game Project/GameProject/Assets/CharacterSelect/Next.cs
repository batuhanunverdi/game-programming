using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Next : MonoBehaviour
{
    public Button nextButton;

    public void ButtonClicked()
    {
        nextButton.interactable = true;
    }

    public void OnClicked(Button button)
    {
        if (button.name == "Female")
        {
            Temp.character = "Female";
        }
        else
        {
            Temp.character = "Male";
        }
    }
}
