using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D rg;
    private bool facingRight = true;
    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }
    public void Move(float speed)
    {
        rg.velocity = new Vector2(speed, rg.velocity.y);

        if((speed > 0f && !facingRight) || (speed < 0f && facingRight)){
            Flip();
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
