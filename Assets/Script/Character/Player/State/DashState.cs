using cfEngine.Util;
using UnityEngine;
using System.Collections.Generic;
public class DashState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Idle, CharacterStateId.Move, CharacterStateId.Dash, CharacterStateId.Interact };

    public class Param : StateParam
    {
        public Vector2 direction;
    }

    public override CharacterStateId Id => CharacterStateId.Dash;

    protected internal override void StartContext(StateParam param)
    {
        float faceDirection = 0f;
        Vector2 direction = Vector2.zero;

        if (param is Param p)
        {
            faceDirection = p.direction.x;
            direction = p.direction;
        }
        else
        {
            faceDirection = StateMachine.Controller.LastFaceDirection;
            direction = StateMachine.Controller.LastMoveDirection;
        }

        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.Dash, faceDirection);
        StateMachine.Controller.SetVelocity(direction * StateMachine.Controller.GetDashSpeed());
        StateMachine.Controller.Animation.Play(animationName, true, onPlayFrame: frame =>
        {
            Debug.Log($"Current Playing Animation: {animationName}");
        });
    }
}
