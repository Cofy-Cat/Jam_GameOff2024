using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class IdleState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Idle, CharacterStateId.Move, CharacterStateId.Interact };
    public override CharacterStateId Id => CharacterStateId.Idle;
    protected internal override void StartContext(CharacterStateMachine sm, StateParam param)
    {
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.Idle, sm.Controller.LastFaceDirection);
        sm.Controller.Animation.Play(animationName, true);
        // sm.Controller.Rigidbody.linearVelocity = Vector2.zero;
        sm.Controller.SetVelocity(Vector2.zero);
    }
} 