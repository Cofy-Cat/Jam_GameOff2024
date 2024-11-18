using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class ChaseState : EnemyState
{
    public override HashSet<EnemyStateId> Whitelist { get; } = new() { EnemyStateId.Chase, EnemyStateId.Attack, EnemyStateId.TriggerOff };
    public override EnemyStateId Id => EnemyStateId.Chase;
    protected internal override void StartContext(StateParam param)
    {
        Debug.Log("Enemy - ChaseState StartContext");
        StateMachine.Controller.SetVelocity(Vector2.zero);
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.Chase, StateMachine.Controller.LastFaceDirection);
        StateMachine.Controller.Animation.Play(animationName, true);
        // Player moveSpeed set to 2f 
        StateMachine.Controller.changePlayerMoveSpeed();
    }

    public override void _Update()
    {
        if (!StateMachine.Controller.ifPlayerIsNear())
        {
            StateMachine.TryGoToState(EnemyStateId.TriggerOff);
        }
    }

    protected internal override void OnEndContext()
    {
        StateMachine.Controller.changePlayerMoveSpeed();
        Debug.Log("Enemy - ChaseState OnEndContext");
    }
}