using cfEngine.Util;
using UnityEngine;

public class InteractState : CharacterState
{
    public override CharacterStateId Id => CharacterStateId.Interact;

    protected internal override void StartContext(CharacterStateMachine sm, StateParam param)
    {
        sm.Controller.Rigidbody.linearVelocity = Vector2.zero;
        // sm.Controller.Interacting = true;
    }

    protected internal override void OnEndContext()
    {
        // sm.Controller.Interacting = false;
    }
}