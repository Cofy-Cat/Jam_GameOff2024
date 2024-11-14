using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class ChaseState : EnemyState
{
    public override HashSet<EnemyStateId> Whitelist { get; } = new() { EnemyStateId.Chase, EnemyStateId.Attack };
    public override EnemyStateId Id => EnemyStateId.Chase;
    protected internal override void StartContext(StateParam param)
    {
        Debug.Log("Enemy - ChaseState StartContext");
        StateMachine.Controller.SetVelocity(Vector2.zero);
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.Chase, StateMachine.Controller.LastFaceDirection);
        StateMachine.Controller.Animation.Play(animationName, true);
    }
}