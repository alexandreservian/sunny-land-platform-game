using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterControllerBase : MonoBehaviour
{
    public Rigidbody2D rb { get; set; }
    private BoxCollider2D boxCollider;
    
    [SerializeField] private float groundCheckDistance = 0.05f;
    
    
    [Header("Run")]
    [SerializeField] public float runSpeed = 1f;

    [Header("Jump")]
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] public float fallMultiplier = 1f;
    [SerializeField] public float lowJumpFallMultiplier = 1f;
    [SerializeField] public LayerMask platformLayerMask;
    

    [Header("Slopes")]
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] public PhysicsMaterial2D noFrictionMaterial;
    [SerializeField] public PhysicsMaterial2D frictionMaterial;
    private float slopeAngle;
    private Vector2 perpenticularSpeed;

    [Header("Knock Back")]
    [SerializeField] [Range(0, 4)] int knockBackForceX = 0;
    [SerializeField] [Range(0, 4)] int knockBackForceY = 0;
    private float knockBackTimeCounter = 0;
    public bool facingRight { get; set; } = true;
    public bool isJumping { get; set; }
    public float horizontalMove { get; set; }
    public bool isJumpButtonPressed { get; set; }
    public bool isJumpButtonPressing { get; set; }
    public float initalGravityScale { get; set; }
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        initalGravityScale = rb.gravityScale;
    }
    public void Move(float characterHorizontalMove, bool jumpButtonPressed, bool jumpButtonPressing)
    {
        horizontalMove = characterHorizontalMove;
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

    public bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, groundCheckDistance, platformLayerMask);
        return raycastHit2d.collider != null;
    }

    public RaycastHit2D GetHitSlope() {
        return Physics2D.Raycast(transform.position, Vector2.down, slopeCheckDistance, platformLayerMask);
    }

    public RaycastHit2D GetHitSlopeLeft() {
        return Physics2D.Raycast(GetStartPositionHitSlope()["left"], Vector2.down, slopeCheckDistance, platformLayerMask);
    }

    public RaycastHit2D GetHitSlopeRight() {
        return Physics2D.Raycast(GetStartPositionHitSlope()["right"], Vector2.down, slopeCheckDistance, platformLayerMask);
    }

    public bool IsOnSlopes() {
        if(GetHitSlope()) {
            float slopeAngle = Vector2.Angle(GetHitSlope().normal, Vector2.up);
            return slopeAngle != 0;
        }
        return false;
    }

    private Dictionary<string, Vector2> GetStartPositionHitSlope() {
        float left = boxCollider.bounds.center.x - boxCollider.bounds.extents.x;
        float right = boxCollider.bounds.center.x + boxCollider.bounds.extents.x;
        float bottom = boxCollider.bounds.center.y - boxCollider.bounds.extents.y;
        Vector2 startPositionLeft = new Vector2(left, bottom);
        Vector2 startPositionRight = new Vector2(right, bottom);
        return new Dictionary<string, Vector2>() {
            {"left", startPositionLeft},
            {"right", startPositionRight}
        };
    }

    public bool IsOnSlopesLeft() {
        if(GetHitSlopeLeft()) {
            float slopeAngle = Vector2.Angle(GetHitSlopeLeft().normal, Vector2.up);
            return slopeAngle != 0;
        }
        return false;
    }

    public bool IsOnSlopesRight() {
        if(GetHitSlopeRight()) {
            float slopeAngle = Vector2.Angle(GetHitSlopeRight().normal, Vector2.up);
            return slopeAngle != 0;
        }
        return false;
    }

    public void KnockBack(float knockBackTime) {
        var forceX = facingRight ? -knockBackForceX : knockBackForceX;
        var forceY = IsGrounded() ? knockBackForceY : rb.velocity.y;
        rb.velocity = new Vector2(forceX, forceY);
        knockBackTimeCounter = knockBackTime;
    }

    private void OnDrawGizmos() {
        boxCollider = boxCollider == null ? GetComponent<BoxCollider2D>() : boxCollider;
        Color rayColor;
        if(IsGrounded()){
            rayColor = Color.green;
        }else{
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + groundCheckDistance), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + groundCheckDistance), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + groundCheckDistance), Vector2.right * (boxCollider.bounds.extents.x) * 2, rayColor);
        // Debug.DrawRay(transform.position, Vector2.down * slopeCheckDistance, Color.blue);
        Debug.DrawRay(GetStartPositionHitSlope()["left"], Vector2.down * slopeCheckDistance, Color.blue);
        Debug.DrawRay(GetStartPositionHitSlope()["right"], Vector2.down * slopeCheckDistance, Color.yellow);
    }
}
