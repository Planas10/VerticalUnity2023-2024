using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Level bool
    public bool Level1On;
    public bool Level2On;

    //Scripts
    [SerializeField] private CameraS CamS;
    [SerializeField] private PlayerMovement PlayerS;
    
    //MINIGAME 1
    //UI
    public GameObject InteractM1Text;
    public GameObject M1Canvas;
    public GameObject InteractText;
    public GameObject S06K0EHint;
    public GameObject S108Hint;
    public GameObject door1Text;

    //GameObjects
    [SerializeField] private GameObject firstDoor;
    
    //Scripts
    [SerializeField] private MinigameControler minigame1Controler;

    //bools
    public bool lookingDoor1;
    public bool InteractingM1;
    public bool DoingM1 = false;
    public bool InteractM1HintsText;
    public bool Interacting1Hint1 = false;
    public bool InteractingM1Hint2 = false;

    //Vectors
    private Vector3 firstDoorLimit;


    //MINIGAME 2
    //Scripts
    [SerializeField] private Minigame2Controller minigame2Controller;

    //UI
    public GameObject generalM2Canvas;
    public GameObject openDoorText;
    public GameObject cantOpenDoorText;
    public GameObject PanelsText;
    public GameObject P1Status;
    public GameObject P2Status;

    //GameObjects
    public GameObject InteractM2PanelsText;
    [SerializeField] private GameObject Door2Object;

    //bools
    public bool InteractM2Panels;
    public bool M2Panel1;
    public bool M2Panel2;
    public bool lookingM2Door;
    public bool interactingM2Door;
    public bool doorCanOpen;

    //Vectors
    private Vector3 Door2Limit;

    private void Awake()
    {
        firstDoorLimit = firstDoor.transform.position - firstDoor.transform.up * 3f;
        Door2Limit = Door2Object.transform.position - Door2Object.transform.up * 3f;
        Level1On = true;
        Level2On = false;
    }

    private void Update()
    {
        //minigame1
        ActivateInteractTexts();
        ActivateMinigame1Hint1();
        ActivateMinigame1Hint2();
        ActivateM1();
        OpenFirstDoor();

        //minigame2
        ActivateTimerM2UI();
        M2DoorManager();
        ActivateM2Canvas();
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

    private void ActivateM2Canvas() {
        if (Level2On)
        {
            generalM2Canvas.SetActive(false);
        }
        else
        {
            generalM2Canvas.SetActive(false);
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

    public void ExitGame() { Application.Quit(); }

    public void ExitM1GUI() {
        minigame1Controler.currentPassword = "";
        DoingM1 = false;
        Cursor.lockState = CursorLockMode.Locked;
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

    private void OpenDoor2() {    
        Level1On = false;
        if (Vector3.Distance(Door2Limit, Door2Object.transform.position) > 0.1f)
        {
            Door2Object.transform.position -= Door2Object.transform.up * 1f * Time.deltaTime;
        }
        
    }

    private void ActivateTimerM2UI() {
        if (minigame2Controller.Panel1On == true || minigame2Controller.Panel2On == true)
        {
            minigame2Controller.TimerObject.SetActive(true);
        }
        else
        {
            minigame2Controller.TimerObject.SetActive(false);
        }
    }

    private void M2DoorManager() {
        if (lookingM2Door)
        {
            openDoorText.SetActive(true);
        }
        else { openDoorText.SetActive(false); }
        if (interactingM2Door)
        {
            if (doorCanOpen)
            {
                if (interactingM2Door) { OpenDoor2(); }
            }
            else
            {
                openDoorText.SetActive(false);
                cantOpenDoorText.SetActive(true);
                StartCoroutine(DeactivateCantOpenText());
            }
        }
    }

    private IEnumerator DeactivateCantOpenText()
    {
        yield return new WaitForSeconds(2f);
        cantOpenDoorText.SetActive(false);
    }

    public void RichardMode() {
        if (Level1On && !minigame1Controler.passwordIsCorrect){ minigame1Controler.passwordIsCorrect = true; Debug.Log("Modo richard"); }
        if (Level2On && !doorCanOpen) { 
            minigame2Controller.Panel1On = true;
            minigame2Controller.Panel2On = true;
            interactingM2Door = true;
        }
    }
}
