using UnityEngine;

public class CharacterIdleState : CharacterBaseState
{   
    public override void EnterState(CharacterController2D character)
    {

    }
    public override void FixedUpdateState(CharacterController2D character)
    {
        
        if(character.IsGrounded()) {
            character.isJumping = false;
        }


        if(character.IsOnSlopes()) {
            character.rb.sharedMaterial = character.frictionMaterial;
        }

        character.rb.velocity = new Vector2(0, character.rb.velocity.y);

        if(character.IsGrounded() && character.isJumpButtonPressed){
            character.TransitionState(character.JumpingState);
        }

        if(character.horizontalMove != 0) {
            character.TransitionState(character.RunningState);
        }
    }
}
