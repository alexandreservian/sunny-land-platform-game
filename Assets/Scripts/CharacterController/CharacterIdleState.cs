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

        if(character.IsGrounded() && character.isJumpButtonPressed){
            character.TransitionState(character.JumpingState);
        }
    }
}
