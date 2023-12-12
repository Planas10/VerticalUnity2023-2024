using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    //Scripts
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Minigame2Controller M2Controller;

    //Rotation
    [SerializeField] private float rotationSpeed = 500f;
    private Camera _mainCamera;

    //Move
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    //Gravity
    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    //Jump
    [SerializeField] private float jumpPower;
    private int _numberOfJumps;
    [SerializeField] private int maxNumberOfJumps = 1;

    [SerializeField] private float speed;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        ApplyGravity();
        if (gameManager.DoingM1 == false)
        {
            ApplyMovement();
            ApplyRotation();
        }

        CheckInteractuable();
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _velocity;
    }

    private void ApplyMovement() {
        _characterController.Move(speed * Time.deltaTime * _direction);
    }
    
    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0) return;

        _direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(_input.x, 0.0f, _input.y);
        var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded() && _numberOfJumps >= maxNumberOfJumps) return;
        if (_numberOfJumps == 0) StartCoroutine(WaitForLanding());

        _numberOfJumps++;
        _velocity = jumpPower;
    }
    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);

        _numberOfJumps = 0;
    }

    //Check if detected GameObject can be interacted with
    private void CheckInteractuable() {
        //if Raycast detects nothing
        if (!Physics.Raycast(transform.position, _mainCamera.transform.forward, out RaycastHit hit, 3f))
        {
            gameManager.InteractingM1 = false;
            gameManager.InteractM1HintsText = false;
            gameManager.lookingDoor1 = false;
            gameManager.InteractM2Panels = false;
            gameManager.lookingM2Door = false;
            return;
        }

        //if Raycast detects something
        if (hit.collider.CompareTag("Minigame1")) { gameManager.InteractingM1 = true; }
        else { gameManager.InteractingM1 = false; }

        if (hit.collider.CompareTag("Minigame1Hint1") || hit.collider.CompareTag("Minigame1Hint2")) { gameManager.InteractM1HintsText = true; }
        else { gameManager.InteractM1HintsText = false; }

        if (hit.collider.CompareTag("Door1")) { gameManager.lookingDoor1 = true; }
        else { gameManager.lookingDoor1 = false; }

        if (gameManager.Level2On == true)
        {
            if (hit.collider.CompareTag("Minigame2Panel1") || hit.collider.CompareTag("Minigame2Panel2")) { gameManager.InteractM2Panels = true; }
            else { gameManager.InteractM2Panels = false; }
        }
        else { gameManager.InteractM2Panels = false; }

        if (hit.collider.CompareTag("Door2")) { gameManager.lookingM2Door = true; }
        else { gameManager.lookingM2Door = false; }
    }

    //function to do when interacting with GameObject
    public void Interact() {
        if (Physics.Raycast(transform.position, _mainCamera.transform.forward, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Minigame1Hint1"))
            {
                gameManager.Interacting1Hint1 = true;
                gameManager.InteractM1HintsText = false;

            }
            else { gameManager.Interacting1Hint1 = false; }

            if (hit.collider.CompareTag("Minigame1Hint2"))
            {
                gameManager.InteractingM1Hint2 = true;
                gameManager.InteractM1HintsText = false;
            }
            else { gameManager.InteractingM1Hint2 = false; }

            if (hit.collider.CompareTag("Minigame1"))
            {
                gameManager.DoingM1 = true;
            }
            else { gameManager.DoingM1 = false; }

            if (hit.collider.CompareTag("Minigame2Panel1")) { M2Controller.Panel1On = true; }
            if (hit.collider.CompareTag("Minigame2Panel2")) { M2Controller.Panel2On = true; }

            if (hit.collider.CompareTag("Door2")) { gameManager.interactingM2Door = true; }
            else { gameManager.interactingM2Door = false; }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Level2Init"))
        {
            gameManager.Level2On = true;
        }
        if (other.gameObject.CompareTag("Level2End"))
        {
            gameManager.Level2On = false;
        }
    }

    //Check if player is on ground
    private bool IsGrounded() => _characterController.isGrounded;
}
