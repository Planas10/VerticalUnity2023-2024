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
    public string currentPassword = "";
    private string correctPassword = "060108";
    public bool passwordIsCorrect = false;

    //Minigame 1
    public void OnClickPassNumber(string s) {
        currentPassword += s;
        PasswordText.text = currentPassword;
    }

    public void OnClickGoButton() {
        if (currentPassword == correctPassword)
        {
            passwordIsCorrect = true;
            PasswordChecked.color = Color.green;
            PasswordChecked.text = "code accepted \n opening door";
        }
        else
        {
            passwordIsCorrect = false;
            PasswordChecked.color = Color.red;
            PasswordChecked.text = "incorrect code \n try again";
        }
        currentPassword = "";
    }
}
