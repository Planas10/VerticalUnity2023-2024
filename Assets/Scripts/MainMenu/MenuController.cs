using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject SettingsCanvas;
    [SerializeField] private GameObject CreditsCanvas;

    private void Awake()
    {
        MainMenuCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
    //Play
    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    //Settings
    public void SettingsButton() {
        SettingsCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }

    public void SettingsReturn() {
        SettingsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    //Credits
    public void CreditsButton() {
        CreditsCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }

    public void CreditsReturn() {
        CreditsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    //Exit game
    public void Exit() {
        Application.Quit();
    }
}
