using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClimbingLatterState : CharacterBaseState
{
    public override void EnterState(CharacterController2D character)
    {

    }
    public override void FixedUpdateState(CharacterController2D character)
    {
        character.rb.velocity = new Vector2(0, character.verticalMove * character.speedClimbLatter);
        character.isClimbingLatter = true;
        character.rb.gravityScale = 0;

        if(character.characterCollision.IsGrounded() && character.verticalMove <= 0) {
            ResetClimb(character);
            character.TransitionState(character.IdleState);
            return;
        }

        if(!character.characterCollision.IsGrounded() && !character.characterCollision.IsLadder()) {
            ResetClimb(character);
            character.TransitionState(character.JumpingState);
            return;
        }
    }

    private void ResetClimb(CharacterController2D character) {
        character.rb.gravityScale = character.initalGravityScale;
        character.isClimbingLatter = false;
    }
}
