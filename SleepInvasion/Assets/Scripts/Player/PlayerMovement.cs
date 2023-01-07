using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;

    private AudioSource _walkAudioSource;
    private float gravity = 20f;
    private Vector3 _velocity;
    private bool _isGrounded;
    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;

    private GameController _gameController;
    private Vector3 _lastPos = Vector3.zero;

    private void Start()
    {
        _gameController = GameController.Instance;
        _walkAudioSource = GetComponent<AudioSource>();
        GameManager.Instance.AudioManager.play(SoundName.WalkStone);
        _walkAudioSource.enabled = false;
    }

    void Update()
    {
        if (_gameController.keysDisabled)
        {
            return;
        }
        if (!controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        vertical = movementSpeed * Input.GetAxis("Vertical");
        horizontal = movementSpeed * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * vertical) + (right * horizontal);

        if (Vector3.Distance(transform.position, _lastPos) > .01f && controller.isGrounded)
        {
            _walkAudioSource.enabled = true;   
        }
        else
        {
            _walkAudioSource.enabled = false;
        }
        _lastPos = transform.position;
        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            moveDirection.y = jumpHeight;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        controller.Move(moveDirection * Time.deltaTime);

        // if (Cursor.lockState == CursorLockMode.Locked && canMove)
        // {
        //     Lookvertical = -Input.GetAxis("Mouse Y");
        //     Lookhorizontal = Input.GetAxis("Mouse X");
        //     rotationX += Lookvertical * lookSpeed;
        //     rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        //     Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        //     transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);
        //     if (isRunning && Moving) cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, RunningFOV, SpeedToFOV * Time.deltaTime);
        //     else cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, SpeedToFOV * Time.deltaTime);
        // }
        
        // // check if the player is on the ground to reset fall speed
        // _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //
        // if (Input.GetButtonDown("Jump") && _isGrounded)
        // {
        //     _velocity.y = jumpHeight;
        // }
        // else
        // {
        //     _velocity.y += gravity * Time.deltaTime;
        // }
        // controller.Move(_velocity * Time.deltaTime);
        //
        // // move player through user input
        // float x = Input.GetAxis("Horizontal");
        // float z = Input.GetAxis("Vertical");
        //
        //
        // var cachedTransform = transform;
        // Vector3 move = cachedTransform.right * x + cachedTransform.forward * z;
        // controller.Move(movementSpeed * Time.deltaTime * move);
    }

    public void DisableWalkAudio()
    {
        _walkAudioSource.enabled = false;
    }
    // private void OnCollisionEnter(Collision collision)
    // {
    //     if(!_isGrounded)
    //         _isGrounded = true;
    // }
}
