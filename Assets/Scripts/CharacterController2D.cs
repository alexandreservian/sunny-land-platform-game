using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool facingRight = true;
    [SerializeField] private float extraHeightText = 0.05f;
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
        boxCollider = GetComponent<BoxCollider2D>();
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
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        return raycastHit2d.collider != null;
    }

    
}
