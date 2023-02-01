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
        perpenticularSpeed = character.characterCollision.GetPerpendicularSlope(character.GetDirection());
        speed = character.horizontalMove * character.runSpeed;

        if((speed > 0f && !character.facingRight) || (speed < 0f && character.facingRight)){
            character.Flip();
        }

        if(character.characterCollision.IsGrounded()) {
            character.isJumping = false;
        }

        if(character.CharacterIsOnSlopes()) {
            character.rb.sharedMaterial = character.noFrictionMaterial;
        }
        
        if(character.CharacterIsOnSlopes()) {
            character.rb.velocity = new Vector2(-speed * perpenticularSpeed.x, -speed * perpenticularSpeed.y);
        } else {
            character.rb.velocity = new Vector2(speed, character.rb.velocity.y);
        }

        if(character.characterCollision.IsOnArrestas(character.GetDirection())){
            character.rb.velocity = new Vector2(speed, 0);
        }

        if((character.characterCollision.IsGrounded() && character.isJumpButtonPressed) || !character.characterCollision.IsGrounded()){
            character.TransitionState(character.JumpingState);
            return;
        }

        if(character.horizontalMove == 0) {
            character.TransitionState(character.IdleState);
            return;
        }

        if(character.characterCollision.IsLadder() && character.verticalMove != 0){
            character.TransitionState(character.ClimbingLatterState);
            return;
        }
    }
}
