using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class TriggerOffState : EnemyState
{
    public override HashSet<EnemyStateId> Whitelist { get; } = new() { EnemyStateId.TriggerOff, EnemyStateId.Chase };
    public override EnemyStateId Id => EnemyStateId.TriggerOff;
    protected internal override void StartContext(StateParam param)
    {
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.TriggerOff, StateMachine.Controller.LastFaceDirection);
        StateMachine.Controller.Animation.Play(animationName, false, onAnimationEnd: () =>
        {
            Debug.Log("FlyingEye TriggerOn Animation ends. Force Go To Chase State");
            StateMachine.ForceGoToState(EnemyStateId.EnemyIdle);
            Debug.Log("This line should not be printed");
        });
    }
}