using UnityEngine;

public class CharacterIdleState : CharacterBaseState
{   
    public override void EnterState(CharacterController2D character)
    {

    }
    public override void FixedUpdateState(CharacterController2D character)
    {
        
        if(character.characterCollision.IsGrounded()) {
            character.isJumping = false;
        }

        if(character.CharacterIsOnSlopes()) {
            character.rb.sharedMaterial = character.frictionMaterial;
        }

        character.rb.velocity = Vector2.zero;

        if(character.characterCollision.IsLadder() && character.verticalMove != 0){
            character.TransitionState(character.ClimbingLatterState);
            return;
        }

        if(character.characterCollision.IsGrounded() && character.isJumpButtonPressed){
            character.TransitionState(character.JumpingState);
            return;
        }

        if(character.horizontalMove != 0) {
            character.TransitionState(character.RunningState);
            return;
        }
    }
}
