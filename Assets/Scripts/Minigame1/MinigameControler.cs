using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameControler : MonoBehaviour
{
    //Scripts
    [SerializeField] private GameManager gameManager;

    //UI
    [SerializeField] private Text PasswordChecked;
    [SerializeField] private Text PasswordText;
    

    //variables
    private string currentPassword = "";
    private string correctPassword = "060108";
    public bool passwordIsCorrect = false;

    public void OnClickPassNumber(string s) {
        currentPassword += s;
        PasswordText.text = currentPassword;
    }

    public void OnClickGoButton() {
        if (currentPassword == correctPassword)
        {
            passwordIsCorrect = true;
            PasswordChecked.color = Color.green;
            PasswordChecked.text = "correct password \n the door is open";
        }
        else
        {
            passwordIsCorrect = false;
            PasswordChecked.color = Color.red;
            PasswordChecked.text = "incorrect password \n try again";
        }
        currentPassword = "";
    }
}
