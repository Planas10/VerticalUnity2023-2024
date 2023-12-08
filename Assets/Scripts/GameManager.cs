using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //UI
    public GameObject InteractText;
    public GameObject M1Canvas;
    public GameObject InteractTextCodes;
    public GameObject S06K0EHint;
    public GameObject S108Hint;

    //Scripts
    [SerializeField] private PlayerMovement PlayerS;
    [SerializeField] private CameraS CamS;
    [SerializeField] private MinigameControler minigameControler;

    //Minigame 1
    public bool InteractingM1;
    public bool DoingM1 = false;
    public bool InteractingCodes;
    public bool Interacting1Hint1 = false;
    public bool InteractingM1Hint2 = false;

    private Vector3 firstDoorLimit;

    //GameObjects
    [SerializeField] private GameObject firstDoor;
    //[SerializeField] private GameObject firstDoorLimit;

    private void Awake()
    {
        firstDoorLimit = firstDoor.transform.position - firstDoor.transform.up * 3f;
    }

    private void Update()
    {
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
            InteractText.SetActive(false);
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
        if (InteractingM1) { InteractText.SetActive(true); }
        else { InteractText.SetActive(false); }

        if (InteractingCodes) { InteractTextCodes.SetActive(true); }
        else { InteractTextCodes.SetActive(false); }
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
        if (minigameControler.passwordIsCorrect)
        {
            if (Vector3.Distance(firstDoorLimit, firstDoor.transform.position) > 0.1f)
            {
                firstDoor.transform.position -= firstDoor.transform.up * 1f * Time.deltaTime; 
            }
        }
    }
}
