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
        var limitTopLatter = !character.characterCollision.HasLadderUpHead() && character.verticalMove >= 0;
        var limitBottomLatter = !character.characterCollision.HasLadderDownFoot() && character.verticalMove <= 0;
        var limitLatter = limitTopLatter || limitBottomLatter;

        if(limitLatter){
            ResetClimb(character);
            character.TransitionState(character.IdleState);
            return;
        }

        if(!character.characterCollision.IsGrounded() && limitLatter) {
            ResetClimb(character);
            character.TransitionState(character.JumpingState);
            return;
        }

        if(character.isJumpButtonPressed) {
            //character.rb.velocity = Vector2.zero;
            ResetClimb(character);
            //character.rb.AddForce(new Vector2(5, 5), ForceMode2D.Impulse);
            character.TransitionState(character.JumpingState);
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
