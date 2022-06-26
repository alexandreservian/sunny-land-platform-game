using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool facingRight = true;
    [SerializeField] private float groundCheckDistance = 0.05f;
    private float initalGravityScale;
    
    [Header("Run")]
    [SerializeField] private float runSpeed = 1f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 1f;
    [SerializeField] private float lowJumpFallMultiplier = 1f;
    [SerializeField] private LayerMask platformLayerMask;
    private bool isJumping; 

    [Header("Slopes")]
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private PhysicsMaterial2D noFrictionMaterial;
    [SerializeField] private PhysicsMaterial2D frictionMaterial;
    private float slopeAngle;
    private Vector2 perpenticularSpeed;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        initalGravityScale = rb.gravityScale;
    }
    public void Move(float horizontalMove, bool jumpButtonPressed, bool jumpButtonPressing)
    {
        perpenticularSpeed = Vector2.Perpendicular(GetHitSlope().normal).normalized;
        float speed = horizontalMove * runSpeed;
        

        if((speed > 0f && !facingRight) || (speed < 0f && facingRight)){
            Flip();
        }

        if(IsGrounded()) {
            isJumping = false;
        }

        if(IsGrounded() && jumpButtonPressed){
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
        }

        if(rb.velocity.y < 0f && !IsGrounded()) {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0f &&  !jumpButtonPressing && !IsGrounded()) {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        else {
            rb.gravityScale = initalGravityScale;
        }

        if(IsOnSlopes() && speed == 0f) {
            rb.sharedMaterial = frictionMaterial;
        } else {
            rb.sharedMaterial = noFrictionMaterial;
        }

        if(IsOnSlopes() && !isJumping) {
            rb.velocity = new Vector2(-speed * perpenticularSpeed.x, -speed * perpenticularSpeed.y);
        } else {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, groundCheckDistance, platformLayerMask);
        return raycastHit2d.collider != null;
    }

    private RaycastHit2D GetHitSlope() {
        return Physics2D.Raycast(transform.position, Vector2.down, slopeCheckDistance, platformLayerMask);
    }

    private bool IsOnSlopes() {
        if(GetHitSlope()) {
            float slopeAngle = Vector2.Angle(GetHitSlope().normal, Vector2.up);
            return slopeAngle != 0;
        }
        return false;
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
        Debug.DrawRay(transform.position, Vector2.down * slopeCheckDistance, Color.blue);
    }
}
