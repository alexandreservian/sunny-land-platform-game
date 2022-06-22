using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController2D characterController;
    private Animator animator;
    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private bool jumpButtonPressed = false;
    private bool jumpButtonPressing = false;
    private bool jumpTouchButtonPressing = false;
    private float touchButtonHorizontal = 0;

    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetMoviment(InputAction.CallbackContext action) => horizontalMove = action.ReadValue<float>();

    public void SetJumpButtonPressed(InputAction.CallbackContext action) => jumpButtonPressed = action.performed;

    void Update()
    {
        jumpButtonPressing = false;
        SetAnimations();
    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove, jumpButtonPressed, jumpButtonPressing);
        jumpButtonPressed = false;
    }

    void SetAnimations(){
        bool isRunning = horizontalMove != 0;
        bool isJumping = !characterController.IsGrounded();
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", isJumping);
        animator.SetFloat("YVelocity", rb.velocity.y);
    }

    public void SetJumpTouchButton(bool value) {
        jumpTouchButtonPressing = value;
    }

    public void SetTouchButtonHorizontal(float value) {
        touchButtonHorizontal = value;
    }
}
