using UnityEngine;

public class CharacterRunningState : CharacterBaseState
{
    private Vector2 perpenticularSpeed;
    private float speed;
    public override void EnterState(CharacterController2D character)
    {

    }
    public override void FixedUpdateState(CharacterController2D character)
    {
        perpenticularSpeed = Vector2.Perpendicular(character.GetHitSlope().normal).normalized;
        speed = character.horizontalMove * character.runSpeed;

        if((speed > 0f && !character.facingRight) || (speed < 0f && character.facingRight)){
            character.Flip();
        }

        if(character.IsGrounded()) {
            character.isJumping = false;
        }

        if(character.IsOnSlopes()) {
            character.rb.sharedMaterial = character.noFrictionMaterial;
        }

        if(character.IsOnSlopes() && !character.isJumping) {
            character.rb.velocity = new Vector2(-speed * perpenticularSpeed.x, -speed * perpenticularSpeed.y);
        } else {
            character.rb.velocity = new Vector2(speed, character.rb.velocity.y);
        }

        if(character.IsGrounded() && character.isJumpButtonPressed){
            character.TransitionState(character.JumpingState);
        }

        if(character.horizontalMove == 0) {
            character.TransitionState(character.IdleState);
        }
    }
}
