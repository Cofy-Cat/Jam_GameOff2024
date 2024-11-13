using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class TriggerOnState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.TriggerOn, CharacterStateId.Chase };
    public override CharacterStateId Id => CharacterStateId.TriggerOn;
    protected internal override void StartContext(StateParam stateParam)
    {
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.TriggerOn, StateMachine.Controller.LastFaceDirection);
        StateMachine.Controller.Animation.Play(animationName, false, onAnimationEnd: () =>
        {
            Debug.Log("FlyingEye TriggerOn Animation ends. Force Go To Chase State");
            StateMachine.Controller.isTriggered = true;
            Debug.Log("This line should not be printed");
        });
    }
}