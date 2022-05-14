using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController2D characterController;
    private Animator animator;
    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private bool jumpButtonPressed = false;
    private bool jumpButtonPressing = false;

    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        jumpButtonPressed = jumpButtonPressed || Input.GetButtonDown("Jump");
        jumpButtonPressing = Input.GetButton("Jump");
        SetAnimations();
    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove, jumpButtonPressed, jumpButtonPressing);
        jumpButtonPressed = false;
    }

    void SetAnimations(){
        bool isRunning = horizontalMove != 0;
        bool isJumping = rb.velocity.y != 0;
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", isJumping);
        animator.SetFloat("YVelocity", rb.velocity.y);
    }
}
