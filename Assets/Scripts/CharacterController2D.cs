using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D rg;
    private bool facingRight = true;
    private CapsuleCollider2D capsuleCollider;
    public LayerMask platformLayerMask;
    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    public void Move(float speed, bool isJumping, float jumpForce)
    {
        rg.velocity = new Vector2(speed, rg.velocity.y);

        if((speed > 0f && !facingRight) || (speed < 0f && facingRight)){
            Flip();
        }

        if(IsGrounded() && isJumping){
            rg.AddForce(new Vector2(0, jumpForce));
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private bool IsGrounded(){
        float extraHeightText = 0.05f;
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        Color rayColor;
        if(raycastHit2d.collider != null){
            rayColor = Color.green;
        }else{
            rayColor = Color.red;
        }
        Debug.DrawRay(capsuleCollider.bounds.center + new Vector3(capsuleCollider.bounds.extents.x, 0), Vector2.down * (capsuleCollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(capsuleCollider.bounds.center - new Vector3(capsuleCollider.bounds.extents.x, 0), Vector2.down * (capsuleCollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(capsuleCollider.bounds.center - new Vector3(capsuleCollider.bounds.extents.x, capsuleCollider.bounds.extents.y + extraHeightText), Vector2.right * (capsuleCollider.bounds.extents.x) * 2, rayColor);
        return raycastHit2d.collider != null;
    }
}
