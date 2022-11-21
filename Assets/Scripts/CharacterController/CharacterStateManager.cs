using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    CharacterBaseState currentState;
    CharacterIdleState IdleState = new CharacterIdleState();
    CharacterRunningState RunningState = new CharacterRunningState();

    void Start() {
        currentState = IdleState;
         currentState.EnterState(this);
    }

    void FixedUpdate() {
        
    }

    protected void TransitionState(CharacterBaseState state) {
        currentState = state;
        currentState.EnterState(this);
    }
}
