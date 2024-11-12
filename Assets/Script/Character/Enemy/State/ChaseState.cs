using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class ChaseState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Chase, CharacterStateId.Attack };
    public override CharacterStateId Id => CharacterStateId.Chase;
    protected internal override void StartContext(CharacterStateMachine sm, StateParam param)
    {
        sm.Controller.SetVelocity(Vector2.zero);
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.TriggerOn, sm.Controller.LastFaceDirection);
        sm.Controller.Animation.Play(animationName, true);
    }
}