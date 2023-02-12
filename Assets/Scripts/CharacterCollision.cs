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
    private Vector3 boundsCenter;
    private Vector3 boundsExtents;
    private Vector3 colliderSize;

    [Header("Ground Check")]
    [SerializeField] [Range(0,0.10f)] private float groundCheckDistance = 0.09f;
    [SerializeField] private LayerMask platformMaskLayer;

    [Header("Ladder Check")]
    [SerializeField] private LayerMask platformLadderLayer;
    [SerializeField] [Range(0,0.5f)] private float checkRadius = 0.1f ;

    [Header("Slopes")]
    [SerializeField] [Range(0,1)] private float slopeCheckDistance = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        boundsCenter = boxCollider.bounds.center;
        boundsExtents = boxCollider.bounds.extents;
        colliderSize = boxCollider.size;
    }

    private Dictionary<Direction, Vector2> GetStartDirectionHitSlope() {
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
        var origin = new Vector2(boundsCenter.x, boundsCenter.y - boundsExtents.y - (groundCheckDistance / 2));
        var size = new Vector2(colliderSize.x, groundCheckDistance);
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(origin, size, 0f, Vector2.down, groundCheckDistance, platformMaskLayer);
        return raycastHit2d.collider != null;
    }

    public bool HasLadderUpHead() {
        var position = new Vector2(boundsCenter.x, boundsCenter.y + boundsExtents.y - checkRadius);
        return Physics2D.OverlapCircle(position, checkRadius, platformLadderLayer);
    }

    public bool HasLadderDownFoot() {
        var position = new Vector2(boundsCenter.x, boundsCenter.y - boundsExtents.y - checkRadius - 0.1f);
        return Physics2D.OverlapCircle(position, checkRadius, platformLadderLayer);
    }

    public bool IsLadder(float verticalMove) {
        var canClimbingLatter = (HasLadderUpHead() && verticalMove == 1) || (HasLadderDownFoot() && verticalMove == -1);
        return canClimbingLatter;
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
        var colliderSize = boxCollider.size;
        Gizmos.color = IsGrounded() ? new Color(0, 1, 0, 0.4f) : new Color(1, 0, 0, 0.4f);
        // Is Grounded
        Gizmos.DrawCube(new Vector2(boundsCenter.x, boundsCenter.y - boundsExtents.y - (groundCheckDistance / 2)), new Vector3(colliderSize.x, groundCheckDistance, 0));
        // Slopes
        Debug.DrawRay(GetStartDirectionHitSlope()[Direction.Right], Vector2.down * slopeCheckDistance, Color.blue);
        Debug.DrawRay(GetStartDirectionHitSlope()[Direction.Left], Vector2.down * slopeCheckDistance, Color.yellow);
        // Ladder
        Gizmos.color = HasLadderUpHead() ? new Color(0, 1, 0, 0.4f) : new Color(1, 0, 0, 0.4f);
        Gizmos.DrawWireSphere(new Vector2(boundsCenter.x, boundsCenter.y + boundsExtents.y - checkRadius), checkRadius);
        Gizmos.color = HasLadderDownFoot() ? new Color(0, 1, 0, 0.4f) : new Color(1, 0, 0, 0.4f);
        Gizmos.DrawWireSphere(new Vector2(boundsCenter.x, boundsCenter.y - boundsExtents.y - checkRadius - 0.1f), checkRadius);
    }
}
