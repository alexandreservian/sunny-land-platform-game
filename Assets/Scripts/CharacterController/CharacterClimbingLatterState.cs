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
        if(!character.characterCollision.IsGrounded() && !character.characterCollision.IsLadder()) {
            ResetClimb(character);
            character.TransitionState(character.JumpingState);
            return;
        }

        if(!character.characterCollision.HasLadderUpHead() && character.verticalMove >= 0){
            ResetClimb(character);
            character.TransitionState(character.IdleState);
            return;
        }

        if(!character.characterCollision.HasLadderDownFoot() && character.verticalMove <= 0){
            ResetClimb(character);
            character.TransitionState(character.IdleState);
            return;
        }

        character.rb.velocity = new Vector2(0, character.verticalMove * character.speedClimbLatter);
        character.isClimbingLatter = true;
        character.rb.isKinematic = true;
    }

    private void ResetClimb(CharacterController2D character) {
        character.rb.isKinematic = false;
        character.isClimbingLatter = false;
    }
}
