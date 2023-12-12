using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame2Controller : MonoBehaviour
{
    //Scripts
    [SerializeField] private GameManager gameManager;

    //UI
    public Text TimerText;
    public Text Panel1Status;
    public Text Panel2Status;

    //GameObjects
    public GameObject TimerObject;
    public GameObject M2Hint;

    public bool Panel1On;
    public bool Panel2On;
    public bool canShowPanelRedText;

    private int initTime = 30;
    private float timer;

    private void Awake()
    {
        timer = initTime;
    }

    private void Update()
    {
        Timer();
        CheckM2Completed();
        PanelStatus();

    }

    private void Timer() {
        if (Panel1On == true || Panel2On == true){
            if (gameManager.Level2On)
            {
                if (timer > 1)
                {
                    timer -= Time.deltaTime;
                    TimerText.text = Mathf.Floor(timer).ToString("");
                }
                else
                {
                    if (!gameManager.doorCanOpen) {
                        Panel1On = false;
                        Panel2On = false;
                    }
                }
                if (canShowPanelRedText)
                {
                    M2Hint.SetActive(true);
                    StartCoroutine(M2HintDisplay());
                }
            }
        }
        else { timer = initTime; }

    }

    private void CheckM2Completed(){
        if (Panel1On == true && Panel2On == true)
        {
            canShowPanelRedText = false;
            gameManager.doorCanOpen = true;
        }
        else { gameManager.doorCanOpen = false; }
    }

    private IEnumerator M2HintDisplay() {

        yield return new WaitForSeconds(5f);
        canShowPanelRedText = false;
        M2Hint.SetActive(false);    
    }

    private void PanelStatus() {
        if (Panel1On) { Panel1Status.text = "on"; Panel1Status.color = Color.green; }
        else { Panel1Status.text = "off"; Panel1Status.color = Color.red; }

        if (Panel2On) { Panel2Status.text = "on"; Panel2Status.color = Color.green; }
        else { Panel2Status.text = "off"; Panel2Status.color = Color.red; }
    }
}
