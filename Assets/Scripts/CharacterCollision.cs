using System;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterCollision : MonoBehaviour
{
    public enum Direction {
        Right,
        Left
    }
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] private LayerMask platformMaskLayer;

    [Header("Ladder Check")]
    [SerializeField] private LayerMask platformLadderLayer;

    [Header("Slopes")]
    [SerializeField] private float slopeCheckDistance;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private Dictionary<Direction, Vector2> GetStartDirectionHitSlope() {
        var boundsCenter = boxCollider.bounds.center;
        var boundsExtents = boxCollider.bounds.extents;
        var right = boundsCenter.x - boundsExtents.x;
        var left = boundsCenter.x + boundsExtents.x;
        var bottom = boundsCenter.y - boundsExtents.y;
        var startDirectionRight = new Vector2(right, bottom);
        var startDirectionLeft = new Vector2(left, bottom);

        return new Dictionary<Direction, Vector2>() {
            {Direction.Right, startDirectionRight},
            {Direction.Left, startDirectionLeft}
        };
    }

    public bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, groundCheckDistance, platformMaskLayer);
        return raycastHit2d.collider != null;
    }

    public bool IsLadder() {
        return boxCollider.IsTouchingLayers(platformLadderLayer);
    }

    private RaycastHit2D GetHitSlope(Direction direction) {
        return Physics2D.Raycast(GetStartDirectionHitSlope()[direction], Vector2.down, slopeCheckDistance, platformMaskLayer);
    }

    private bool HasHitSlope() {
        return GetHitSlope(Direction.Right) && GetHitSlope(Direction.Left);
    }
    public bool IsOnSlopes(Direction direction) {
        if(HasHitSlope()) {
            var hitFrontSlope = GetHitSlope(direction);
            var hitBackSlope = direction == Direction.Right ? GetHitSlope(Direction.Left) : GetHitSlope(Direction.Right);
            var frontAngle = Vector2.Angle(hitFrontSlope.normal, Vector2.up);
            var backAngle = Vector2.Angle(hitBackSlope.normal, Vector2.up);
            var frontPointY = Math.Round(hitFrontSlope.point.y, 2);
            var backPointY = Math.Round(hitBackSlope.point.y, 2);
            return frontAngle != 0 ? true : backAngle != 0 && backPointY > frontPointY ? true : false;
        }
        return false;
    }
    public Vector2 GetPerpendicularSlope(Direction direction) { 
        if(HasHitSlope()) {
            var hitFrontSlope = GetHitSlope(direction);
            var hitBackSlope = direction == Direction.Right ? GetHitSlope(Direction.Left) : GetHitSlope(Direction.Right);
            var frontAngle = Vector2.Angle(hitFrontSlope.normal, Vector2.up);
            var currentSlopes = frontAngle != 0 ? hitFrontSlope : hitBackSlope;
            return Vector2.Perpendicular(currentSlopes.normal).normalized;;
        }
        return Vector2.left;
    }

    public bool IsOnArrestas(Direction direction) {
        if(HasHitSlope()) {
            var hitFrontSlope = GetHitSlope(direction);
            var hitBackSlope = direction == Direction.Right ? GetHitSlope(Direction.Left) : GetHitSlope(Direction.Right);
            var frontPointY = Math.Round(hitFrontSlope.point.y, 2);
            var backPointY = Math.Round(hitBackSlope.point.y, 2);
            return frontPointY >= backPointY;
        }
        return false; 
    }

    private void OnDrawGizmos() {
        boxCollider = boxCollider == null ? GetComponent<BoxCollider2D>() : boxCollider;
        var boundsCenter = boxCollider.bounds.center;
        var boundsExtents = boxCollider.bounds.extents;
        Color rayColor;
        
        if(IsGrounded()){
            rayColor = Color.green;
        }else{
            rayColor = Color.red;
        }
        
        Debug.DrawRay(boundsCenter + new Vector3(boundsExtents.x, 0), Vector2.down * (boundsExtents.y + groundCheckDistance), rayColor);
        Debug.DrawRay(boundsCenter - new Vector3(boundsExtents.x, 0), Vector2.down * (boundsExtents.y + groundCheckDistance), rayColor);
        Debug.DrawRay(boundsCenter - new Vector3(boundsExtents.x, boundsExtents.y + groundCheckDistance), Vector2.right * (boundsExtents.x) * 2, rayColor);
        Debug.DrawRay(GetStartDirectionHitSlope()[Direction.Right], Vector2.down * slopeCheckDistance, Color.blue);
        Debug.DrawRay(GetStartDirectionHitSlope()[Direction.Left], Vector2.down * slopeCheckDistance, Color.yellow);
    }
}
