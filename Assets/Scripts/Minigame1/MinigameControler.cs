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

    //Audio
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource correctSound;
    [SerializeField] private AudioSource incorrectSound;

    //variables
    public string currentPassword = "";
    private string correctPassword = "060108";
    public bool passwordIsCorrect = false;

    //Minigame 1
    public void OnClickPassNumber(string s) {
        buttonSound.Play();
        currentPassword += s;
        PasswordText.text = currentPassword;
    }

    public void OnClickGoButton() {
        if (currentPassword == correctPassword)
        {
            correctSound.Play();
            passwordIsCorrect = true;
            PasswordChecked.color = Color.green;
            PasswordChecked.text = "code accepted \n opening door";
            StartCoroutine(HideM1Canvas());
        }
        else
        {
            incorrectSound.Play();
            passwordIsCorrect = false;
            PasswordChecked.color = Color.red;
            PasswordChecked.text = "incorrect code \n try again";
        }
        currentPassword = "";
        PasswordText.text = "";
    }

    public void OnClickClearButton() {
        currentPassword = "";
        PasswordText.text = "";
    }

    private IEnumerator HideM1Canvas() {
        yield return new WaitForSeconds(3f);
        gameManager.M1Completed = true;
        gameManager.canPlayDoor1Sound = true;
    }
}
