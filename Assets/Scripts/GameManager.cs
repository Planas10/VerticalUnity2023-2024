using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Canvas Texts
    public GameObject InteractText;
    public GameObject SKE006Hint;
    public GameObject S018Hint;

    //Scripts
    [SerializeField] private PlayerMovement PlayerS;

    //Components

    //Variables
    public bool playerCanMove;

    //Minigame 1
    public bool InteractingMinigame;
    public bool InteractingMinigame1Hint1;
    public bool InteractingMinigame1Hint2;




    private void Update()
    {
        //Activate Interact Text;
        if (InteractingMinigame) { InteractText.gameObject.SetActive(true); playerCanMove = false; }
        else { InteractText.gameObject.SetActive(false); playerCanMove = true; }

        //Activate Minigame1 Hint1
        if (InteractingMinigame1Hint1) { SKE006Hint.gameObject.SetActive(true); playerCanMove = false; }
        else { SKE006Hint.gameObject.SetActive(false); playerCanMove = true; }

        //Activate Minigame1 Hint2
        if (InteractingMinigame1Hint2) { S018Hint.gameObject.SetActive(true); playerCanMove = false; }
        else { S018Hint.gameObject.SetActive(false); playerCanMove = true; }

    }
}
