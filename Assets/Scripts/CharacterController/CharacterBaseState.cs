using UnityEngine;

public abstract class CharacterBaseState
{
    public abstract void EnterState(CharacterController2D character);
    public abstract void FixedUpdateState(CharacterController2D character);
}


