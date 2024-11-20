using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class TriggerOnState : EnemyState
{
    public override HashSet<EnemyStateId> Whitelist { get; } = new() { EnemyStateId.TriggerOn, EnemyStateId.Chase };
    public override EnemyStateId Id => EnemyStateId.TriggerOn;
    protected internal override void StartContext(StateParam param)
    {
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.TriggerOn, StateMachine.Controller.LastFaceDirection);
        StateMachine.Controller.Animation.Play(animationName, false, onAnimationEnd: () =>
        {
            Debug.Log("FlyingEye TriggerOff Animation ends. Force Go To Chase State");
            StateMachine.ForceGoToState(EnemyStateId.Chase);
        });
    }
}