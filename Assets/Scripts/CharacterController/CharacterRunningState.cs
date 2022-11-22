using UnityEngine;

public class CharacterRunningState : CharacterBaseState
{
    public override void EnterState(CharacterController2D character)
    {

    }
    public override void FixedUpdateState(CharacterController2D character)
    {
        if(character.IsGrounded()) {
            character.isJumping = false;
        }

        if(character.IsGrounded() && character.isJumpButtonPressed){
            character.TransitionState(character.JumpingState);
        }

        if(character.horizontalMove == 0) {
            character.TransitionState(character.IdleState);
        }

        if(character.IsOnSlopes()) {
            character.rb.sharedMaterial = character.noFrictionMaterial;
        }
    }
}
