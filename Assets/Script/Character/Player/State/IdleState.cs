using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class IdleState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Idle, CharacterStateId.Move, CharacterStateId.Interact};
    public override CharacterStateId Id => CharacterStateId.Idle;
    protected internal override void StartContext(StateParam stateParam)
    {
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.Idle, StateMachine.Controller.LastFaceDirection);
        StateMachine.Controller.Animation.Play(animationName, true);
        StateMachine.Controller.SetVelocity(Vector2.zero);
    }
} 