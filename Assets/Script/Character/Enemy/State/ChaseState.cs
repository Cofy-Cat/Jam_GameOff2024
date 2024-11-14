using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class ChaseState : EnemyState
{
    public override HashSet<EnemyStateId> Whitelist { get; } = new() { EnemyStateId.Chase, EnemyStateStateId.Attack };
    public override EnemyStateId Id => EnemyStateId.Chase;
    protected internal override void StartContext(EnemyStateMachine sm, StateParam param)
    {
        sm.Controller.SetVelocity(Vector2.zero);
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.TriggerOn, sm.Controller.LastFaceDirection);
        sm.Controller.Animation.Play(animationName, true);
    }
}