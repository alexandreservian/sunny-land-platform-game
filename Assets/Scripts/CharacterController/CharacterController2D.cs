using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : CharacterControllerBase
{
    CharacterBaseState currentState;
    public CharacterIdleState IdleState = new CharacterIdleState();
    public CharacterRunningState RunningState = new CharacterRunningState();
    public CharacterJumpingState JumpingState = new CharacterJumpingState();

    void Start() {
        TransitionState(IdleState);
    }

    void FixedUpdate() {
        currentState.FixedUpdateState(this);
    }

    public void TransitionState(CharacterBaseState state) {
        currentState = state;
        currentState.EnterState(this);
    }
}
