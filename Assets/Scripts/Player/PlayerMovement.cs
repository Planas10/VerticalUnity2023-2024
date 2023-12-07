using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Script references
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CameraS camS;

    //Component references
    private CharacterController characterController;

    //This script variables
    [SerializeField] private float speed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Movement();
        PickItem();
    }

    private void Movement() {

        Vector3 MoveDirection = new Vector3(Input.GetAxis("Horizontal"), -gameManager.Gravity, Input.GetAxis("Vertical"));
        MoveDirection = transform.TransformDirection(MoveDirection);
        characterController.Move(MoveDirection * speed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, camS.rotationY, 0);
    }

    private void PickItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
        {
            if (hit.collider.GetComponent<Pickable>()){

                if (Input.GetKeyDown(KeyCode.E)) {
                    hit.collider.gameObject.GetComponent<Pickable>().picked = true;
                }
            }
        }
    }

}
