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

    //Rotation
    [SerializeField] private float rotationSpeed = 500f;
    private Camera _mainCamera;

    //Move
    public bool CanMove;
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    //[SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

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
        ApplyMovement();
        ApplyRotation();
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
        _characterController.Move(_direction * speed * Time.deltaTime);
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

    private void CheckInteractuable() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, _mainCamera.transform.forward, out hit, 3f))
        {
            if (hit.collider.tag == "Minigame1") { gameManager.InteractingM1 = true; }
            else { gameManager.InteractingM1 = false; }
        }
        else { gameManager.InteractingM1 = false; }
        if (hit.collider.tag == "Minigame1Hint1" || hit.collider.tag == "Minigame1Hint2") {
        }
    }

    public void Interact() {
        Debug.Log("Intentando interactuar");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, _mainCamera.transform.forward, out hit, 3f))
        {
            if (hit.collider.tag == "Minigame1Hint1") { 
                gameManager.Interacting1Hint1 = true;
            }
            else { gameManager.Interacting1Hint1 = false; }
            if (hit.collider.tag == "Minigame1Hint2") {
                gameManager.InteractingM1Hint2 = true;
            }
            else { gameManager.InteractingM1Hint2 = false; }
        }
    }

    private bool IsGrounded() => _characterController.isGrounded;
}
