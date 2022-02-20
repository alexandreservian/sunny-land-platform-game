using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    private CharacterController2D characterController;
    private float horizontalMove = 0f;

    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove);
    }
}
