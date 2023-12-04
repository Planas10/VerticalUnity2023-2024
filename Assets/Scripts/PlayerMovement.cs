using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Script references
    [SerializeField] private GameManager gameManager;

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
    }

    private void Movement() {

        Vector3 MoveDirection = new Vector3(Input.GetAxis("Horizontal"), -gameManager.Gravity, Input.GetAxis("Vertical"));
        characterController.Move(MoveDirection * speed * Time.deltaTime);
    }

}
