using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class TriggerOnState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.TriggerOn, CharacterStateId.Chase };
    public override CharacterStateId Id => CharacterStateId.TriggerOn;
    protected internal override void StartContext(CharacterStateMachine sm, StateParam param)
    {
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.TriggerOn, sm.Controller.LastFaceDirection);
        sm.Controller.Animation.Play(animationName, false, onAnimationEnd: () =>
        {
            Debug.Log("FlyingEye TriggerOn Animation ends. Force Go To Chase State");
            sm.Controller.isTriggered = true;
            Debug.Log("This line should not be printed");
        });
    }
}