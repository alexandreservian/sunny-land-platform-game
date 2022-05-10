using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController2D characterController;
    private float horizontalMove = 0f;
    private bool jumpButtonPressed = false;
    private bool jumpButtonPressing = false;

    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        jumpButtonPressed = Input.GetButtonDown("Jump");
        jumpButtonPressing = Input.GetButton("Jump");
    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove, jumpButtonPressed, jumpButtonPressing);
    }
}
