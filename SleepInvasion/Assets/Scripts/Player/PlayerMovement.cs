using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public CharacterController controller;
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    
    private float gravity = -9.81f;
    private Vector3 _velocity;
    private bool _isGrounded;

    private GameController _gameController;

    private void Start()
    {
        _gameController = GameController.Instance;
    }


    void Update()
    {
        if (_gameController.keysDisabled)
        {
            return;
        }
        
        // check if the player is on the ground to reset fall speed
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -1f;
        }

        // apply gravity
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
        
        // move player through user input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        var cachedTransform = transform;
        Vector3 move = cachedTransform.right * x + cachedTransform.forward * z;
        controller.Move(movementSpeed * Time.deltaTime * move);
    }
}