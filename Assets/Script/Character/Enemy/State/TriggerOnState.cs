using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class TriggerOnState : EnemyState
{
    public override HashSet<EnemyStateId> Whitelist { get; } = new() { EnemyStateId.TriggerOn, EnemyStateId.Chase };
    public override EnemyStateId Id => EnemyStateId.TriggerOn;
    protected internal override void StartContext(EnemyStateMachine sm, StateParam param)
    {
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.TriggerOn, sm.Controller.LastFaceDirection);
        sm.Controller.Animation.Play(animationName, false, onAnimationEnd: () =>
        {
            Debug.Log("FlyingEye TriggerOn Animation ends. Force Go To Chase State");
            sm.ForceGoToState(EnemyStateId.Chase);
            Debug.Log("This line should not be printed");
        });
    }
}