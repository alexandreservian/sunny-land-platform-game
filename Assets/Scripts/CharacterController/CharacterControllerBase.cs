using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterCollision))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterControllerBase : MonoBehaviour
{
    public Rigidbody2D rb { get; set; }
    [HideInInspector] public CharacterCollision characterCollision;
    private BoxCollider2D boxCollider;
    
    [Header("Run")]
    [SerializeField] [Range(1, 15)] public float runSpeed = 5f;

    [Header("Climb Latter")]
    [SerializeField] [Range(1, 5)] public float speedClimbLatter = 5f;
    public bool isClimbingLatter { get; set; }

    [Header("Jump")]
    [SerializeField] public float jumpForce = 13f;
    [SerializeField] public float fallMultiplier = 1f;
    [SerializeField] public float lowJumpFallMultiplier = 1f;
    

    [Header("Slopes")]
    [SerializeField] public PhysicsMaterial2D noFrictionMaterial;
    [SerializeField] public PhysicsMaterial2D frictionMaterial;

    [Header("Knock Back")]
    [SerializeField] [Range(0, 4)] int knockBackForceX = 0;
    [SerializeField] [Range(0, 4)] int knockBackForceY = 0;
    private float knockBackTimeCounter = 0;
    public bool facingRight { get; set; } = true;
    public bool isJumping { get; set; }
    public float horizontalMove { get; set; }
    public float verticalMove { get; set; }
    public bool isJumpButtonPressed { get; set; }
    public bool isJumpButtonPressing { get; set; }
    public float initalGravityScale { get; set; }
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        characterCollision = GetComponent<CharacterCollision>();
        boxCollider = GetComponent<BoxCollider2D>();
        initalGravityScale = rb.gravityScale;
    }
    public void Move(float characterHorizontalMove, float characterVerticalMove, bool jumpButtonPressed, bool jumpButtonPressing)
    {
        
        horizontalMove = characterHorizontalMove;
        verticalMove = characterVerticalMove;
        isJumpButtonPressed = jumpButtonPressed;
        isJumpButtonPressing = jumpButtonPressing;

        if(knockBackTimeCounter <= 0) {
        } else {
            knockBackTimeCounter -= Time.deltaTime;
        }
        
    }
    public void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public CharacterCollision.Direction GetDirection() { 
        return transform.localScale.x <= -1 ? CharacterCollision.Direction.Right : CharacterCollision.Direction.Left;
    } 

    public bool CharacterIsOnSlopes() {
        return characterCollision.IsOnSlopes(GetDirection());
    }

    public void KnockBack(float knockBackTime) {
        var forceX = facingRight ? -knockBackForceX : knockBackForceX;
        var forceY = characterCollision.IsGrounded() ? knockBackForceY : rb.velocity.y;
        rb.velocity = new Vector2(forceX, forceY);
        knockBackTimeCounter = knockBackTime;
    }
}
