using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public override HashSet<EnemyStateId> Whitelist { get; } = new() { EnemyStateId.EnemyIdle, EnemyStateId.TriggerOn };
    public override EnemyStateId Id => EnemyStateId.EnemyIdle;
    protected internal override void StartContext(StateParam param)
    {
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.Idle, StateMachine.Controller.LastFaceDirection);
        StateMachine.Controller.Animation.Play(animationName, true);
        StateMachine.Controller.SetVelocity(Vector2.zero);
    }

    public override void _Update()
    {
        if (StateMachine.Controller.ifPlayerIsNear())
        {
            StateMachine.TryGoToState(EnemyStateId.TriggerOn);
        }
    }
}