using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject PauseCanvas;
    [SerializeField] private GameObject PauseSettingsCanvas;

    public void PauseGame()
    {
        PauseCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        gameManager.gameIsPaused = true;
    }

    public void ResumeGame() {
        gameManager.gameIsPaused = false;
        PauseCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseSettingsButton() {
        PauseSettingsCanvas.SetActive(true);
        PauseCanvas.SetActive(false);
    }

    public void PauseSettingsReturn() {
        PauseSettingsCanvas.SetActive(false);
        PauseCanvas.SetActive(true);
    }

    public void PauseMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
