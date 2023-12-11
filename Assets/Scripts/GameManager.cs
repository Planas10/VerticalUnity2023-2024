using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Level bool
    public bool Level1On;
    public bool Level2On;

    //UI
    public GameObject InteractM1Text;
    public GameObject M1Canvas;
    public GameObject InteractText;
    public GameObject S06K0EHint;
    public GameObject S108Hint;
    public GameObject door1Text;

    public GameObject InteractM2PanelsText;

    //Scripts
    [SerializeField] private PlayerMovement PlayerS;
    [SerializeField] private CameraS CamS;
    [SerializeField] private MinigameControler minigame1Controler;
    [SerializeField] private Minigame2Controller minigame2Controller;

    //Minigame 1
    public bool lookingDoor1;
    public bool InteractingM1;
    public bool DoingM1 = false;
    public bool InteractM1HintsText;
    public bool Interacting1Hint1 = false;
    public bool InteractingM1Hint2 = false;

    private Vector3 firstDoorLimit;

    //GameObjects
    [SerializeField] private GameObject firstDoor;

    //Minigame 2
    public bool InteractM2Panels;
    public bool M2Panel1;
    public bool M2Panel2;

    private void Awake()
    {
        firstDoorLimit = firstDoor.transform.position - firstDoor.transform.up * 3f;
        Level1On = true;
        Level2On = false;
    }

    private void Update()
    {
        Debug.Log(Level2On);

        ActivateInteractTexts();
        ActivateMinigame1Hint1();
        ActivateMinigame1Hint2();
        ActivateM1();

        OpenFirstDoor();
    }

    private void ActivateM1() {
        if (DoingM1)
        {
            //Hide all texts + show minigame UI
            InteractM1Text.SetActive(false);
            S06K0EHint.gameObject.SetActive(false);
            S108Hint.gameObject.SetActive(false);
            M1Canvas.SetActive(true);

            //stop camera movement
            CamS.enabled = false;

            //Allow player to use cursor
            Cursor.lockState = CursorLockMode.None;
        }
        else { 
            M1Canvas.SetActive(false);
            CamS.enabled = true;
        }
    }

    private void ActivateInteractTexts()
    {
        if (InteractingM1) { InteractM1Text.SetActive(true); }
        else { InteractM1Text.SetActive(false); }

        if (InteractM1HintsText) { InteractText.SetActive(true); }
        else { InteractText.SetActive(false); }

        if (lookingDoor1 && minigame1Controler.passwordIsCorrect != true) { door1Text.SetActive(true); }
        else { door1Text.SetActive(false); }

        if (InteractM2Panels) { InteractM2PanelsText.SetActive(true); }
        else { InteractM2PanelsText.SetActive(false); }
    }
    private void ActivateMinigame1Hint1()
    {
        if (Interacting1Hint1 == true)
        {
            S06K0EHint.gameObject.SetActive(true);
            StartCoroutine(WaitForM1Hint1());
        }
    }

    private void ActivateMinigame1Hint2()
    {
        if (InteractingM1Hint2 == true)
        {
            S108Hint.gameObject.SetActive(true);
            StartCoroutine(WaitForM1Hint2());
        }
    }

    private IEnumerator WaitForM1Hint1()
    {
        yield return new WaitForSeconds(2f);
        S06K0EHint.gameObject.SetActive(false);
        Interacting1Hint1 = false;
    }
    private IEnumerator WaitForM1Hint2()
    {
        yield return new WaitForSeconds(2f);
        S108Hint.gameObject.SetActive(false);
        InteractingM1Hint2 = false;
    }

    public void ExitGame() {
        if (!DoingM1)
        {
            Application.Quit();
        }
        else
        {
            DoingM1 = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OpenFirstDoor() {
        if (minigame1Controler.passwordIsCorrect)
        {
            Level1On = false;
            if (Vector3.Distance(firstDoorLimit, firstDoor.transform.position) > 0.1f)
            {
                firstDoor.transform.position -= firstDoor.transform.up * 1f * Time.deltaTime; 
            }
        }
    }

    public void RichardMode() {
        if (!minigame1Controler.passwordIsCorrect){ minigame1Controler.passwordIsCorrect = true; }
    }
}
