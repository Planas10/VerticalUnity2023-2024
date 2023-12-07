using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float Gravity;
    public GameObject InteractText;

    [SerializeField] private PlayerMovement PlayerS;

    private void Update()
    {
        if (PlayerS.InteractingMinigame1) { InteractText.gameObject.SetActive(true); }
        else { InteractText.gameObject.SetActive(false); }

    }
}
