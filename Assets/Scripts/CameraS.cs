using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraS : MonoBehaviour
{
    private float rotationX;
    private float rotationY;
    private void LateUpdate()
    {
        rotationX += Input.GetAxis("Mouse Y");
        rotationY += Input.GetAxis("Mouse X");

        rotationX = Mathf.Clamp(rotationX, -40f, 40f);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
    }

}
