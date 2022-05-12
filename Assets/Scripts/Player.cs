using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController2D characterController;
    private Animator animator;
    private float horizontalMove = 0f;
    private bool jumpButtonPressed = false;
    private bool jumpButtonPressing = false;

    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        jumpButtonPressed = jumpButtonPressed || Input.GetButtonDown("Jump");
        jumpButtonPressing = Input.GetButton("Jump");
        bool isRunning = horizontalMove != 0;
        animator.SetBool("PlayerRun", isRunning);
    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove, jumpButtonPressed, jumpButtonPressing);
        jumpButtonPressed = false;
    }
}
