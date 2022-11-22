using UnityEngine;

public class CharacterJumpingState : CharacterBaseState
{
    public override void EnterState(CharacterController2D character)
    {
        character.isJumping = true;
        character.rb.velocity = Vector2.up * character.jumpForce;
    }
    public override void FixedUpdateState(CharacterController2D character)
    {
        if(character.rb.velocity.y < 0f && !character.IsGrounded()) {
            character.rb.gravityScale = character.fallMultiplier;
        }
        else if (character.rb.velocity.y > 0f &&  !character.isJumpButtonPressing && !character.IsGrounded()) {
            character.rb.gravityScale = character.lowJumpFallMultiplier;
        }
        else {
            character.rb.gravityScale = character.initalGravityScale;
        }
        
        if(character.IsGrounded()) {
            character.TransitionState(character.IdleState);
        }
    }
}
