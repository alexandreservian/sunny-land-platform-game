using UnityEngine;

public class CharacterJumpingState : CharacterBaseState
{
    private float speed;
    public override void EnterState(CharacterController2D character)
    {
        character.isJumping = true;
        character.rb.velocity = Vector2.up * character.jumpForce;
    }
    public override void FixedUpdateState(CharacterController2D character)
    {
        speed = character.horizontalMove * character.runSpeed;

        if((speed > 0f && !character.facingRight) || (speed < 0f && character.facingRight)){
            character.Flip();
        }

        if(character.rb.velocity.y < 0f && !character.characterCollision.IsGrounded()) {
            character.rb.gravityScale = character.fallMultiplier;
        }
        else if (character.rb.velocity.y > 0f &&  !character.isJumpButtonPressing && !character.characterCollision.IsGrounded()) {
            character.rb.gravityScale = character.lowJumpFallMultiplier;
        }
        else {
            character.rb.gravityScale = character.initalGravityScale;
        }
        
        character.rb.velocity = new Vector2(speed, character.rb.velocity.y);

        if(character.characterCollision.IsGrounded()) {
            character.TransitionState(character.IdleState);
        }
    }
}
