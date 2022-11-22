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
    }
}
