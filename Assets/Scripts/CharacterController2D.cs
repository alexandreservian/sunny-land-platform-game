using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private bool facingRight = true;
    private float extraHeightText = 0.05f;
    private float initalGravityScale;
    
    [Header("Run")]
    [SerializeField] private float runSpeed = 1f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 1f;
    [SerializeField] private float lowJumpFallMultiplier = 1f;
    [SerializeField] private LayerMask platformLayerMask;
    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        initalGravityScale = rb.gravityScale;
    }
    public void Move(float horizontalMove, bool jumpButtonPressed, bool jumpButtonPressing)
    {
        float speed = horizontalMove * runSpeed;
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if((speed > 0f && !facingRight) || (speed < 0f && facingRight)){
            Flip();
        }

        if(IsGrounded() && jumpButtonPressed){
            rb.velocity = Vector2.up * jumpForce;
        }

        if(rb.velocity.y < 0f) {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0f &&  !jumpButtonPressing) {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        else {
            rb.gravityScale = initalGravityScale;
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        return raycastHit2d.collider != null;
    }

     private void OnDrawGizmos() {
        capsuleCollider = capsuleCollider == null ? GetComponent<CapsuleCollider2D>() : capsuleCollider;
        Color rayColor;
        if(IsGrounded()){
            rayColor = Color.green;
        }else{
            rayColor = Color.red;
        }
        Debug.DrawRay(capsuleCollider.bounds.center + new Vector3(capsuleCollider.bounds.extents.x, 0), Vector2.down * (capsuleCollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(capsuleCollider.bounds.center - new Vector3(capsuleCollider.bounds.extents.x, 0), Vector2.down * (capsuleCollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(capsuleCollider.bounds.center - new Vector3(capsuleCollider.bounds.extents.x, capsuleCollider.bounds.extents.y + extraHeightText), Vector2.right * (capsuleCollider.bounds.extents.x) * 2, rayColor);
    }
}
