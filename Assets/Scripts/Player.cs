using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    public float jumpForce = 1f;
    private CharacterController2D characterController;
    private float horizontalMove = 0f;
    public bool isJumping = false;

    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        if(Input.GetButtonDown("Jump")){
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove, isJumping, jumpForce);
        isJumping = false;
    }
}
