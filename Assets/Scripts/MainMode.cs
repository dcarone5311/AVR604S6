using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMode : MonoBehaviour
{
    public Button[] buttons;
    private void OnEnable()
    {
        UIController.ShowUI("Main");

        Invoke("EnableButtons", 1f);

    }


    void EnableButtons()
    {
        foreach (var button in buttons)
        {
            button.interactable = true;

        }


    }

}
