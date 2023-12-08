using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Canvas Texts
    public GameObject InteractText;
    public GameObject InteractTextCodes;
    public GameObject S06K0EHint;
    public GameObject S108Hint;

    //Scripts
    [SerializeField] private PlayerMovement PlayerS;

    //Components

    //Variables

    //Minigame 1
    public bool InteractingM1;
    public bool Interacting1Hint1 = false;
    public bool InteractingM1Hint2 = false;


    private void Update()
    {
        ActivateInteractText();
        ActivateMinigame1Hint1();
        ActivateMinigame1Hint2();
    }

    private void ActivateInteractText()
    {
        if (InteractingM1) { InteractText.gameObject.SetActive(true); }
        else { InteractText.gameObject.SetActive(false); }
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
}
