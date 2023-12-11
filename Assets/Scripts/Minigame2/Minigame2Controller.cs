using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame2Controller : MonoBehaviour
{
    //Scripts
    [SerializeField] private GameManager gameManager;

    //UI
    [SerializeField] private Text TimerText;

    public bool Panel1On;
    public bool Panel2On;

    private int initTime = 60;
    private float timer;

    private void Awake()
    {
        timer = initTime;
    }

    private void Update()
    {
        Timer();
    }

    private void Timer() {
        if (Panel1On || Panel2On){ timer -= Time.deltaTime; TimerText.text = timer.ToString(""); }
        else { timer = initTime; }
    }
}
