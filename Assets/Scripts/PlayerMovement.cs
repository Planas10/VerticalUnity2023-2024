using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Camera Cam;

    [SerializeField] private float speed;



    private float mouseHorizontal = 2f;
    private float mouseVertical = 2f;

    private float rotClamped;

    float h_mouse;
    float v_mouse;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MouseMove();
    }
    private void MouseMove()
    {
        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse = mouseVertical * -Input.GetAxis("Mouse Y");

        transform.Rotate(0, h_mouse, 0);

        v_mouse = Mathf.Clamp(Cam.transform.rotation.x, -80, 80);

        Cam.transform.Rotate(v_mouse, 0, 0);


        Debug.Log(rotClamped);
    }

    //private void Movement()
    //{
    //    if (Input.GetButtonDown("Horizontal"))
    //    {
    //    }

    //    if (Input.GetButtonDown("Vertical"))
    //    {
    //    }
    //}

}
