using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraS : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public float rotationX;
    public float rotationY;
    private void LateUpdate()
    {
        rotationX -= Input.GetAxis("Mouse Y");
        rotationY += Input.GetAxis("Mouse X");

        rotationX = Mathf.Clamp(rotationX, -60f, 60f);
        if (gameManager.gameIsPaused == false)
        {
            transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
        }
    }

}
