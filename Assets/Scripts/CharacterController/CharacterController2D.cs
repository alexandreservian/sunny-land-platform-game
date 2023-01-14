using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : CharacterControllerBase
{
    CharacterBaseState currentState;
    public CharacterBaseState IdleState = new CharacterIdleState();
    public CharacterBaseState RunningState = new CharacterRunningState();
    public CharacterBaseState JumpingState = new CharacterJumpingState();
    public CharacterBaseState ClimbingLatterState = new CharacterClimbingLatterState();

    void Start() {
        TransitionState(IdleState);
    }

    void FixedUpdate() {
        currentState.FixedUpdateState(this);
    }

    public void TransitionState(CharacterBaseState state) {
        // Debug.Log(state.GetType());
        currentState = state;
        currentState.EnterState(this);
    }
}
