using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] public LayerMask platformLayerMask;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, groundCheckDistance, platformLayerMask);
        return raycastHit2d.collider != null;
    }

    private void OnDrawGizmos() {
        boxCollider = boxCollider == null ? GetComponent<BoxCollider2D>() : boxCollider;
    }
}
