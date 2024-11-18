using cfEngine.Util;
using UnityEngine;
using System.Collections.Generic;
public class MoveState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Idle, CharacterStateId.Move, CharacterStateId.Interact, CharacterStateId.Activate };

    public class Param : StateParam
    {
        public Vector2 direction;
    }

    public override CharacterStateId Id => CharacterStateId.Move;

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
        animationName = AnimationName.GetDirectional(AnimationName.Move, faceDirection);
        StateMachine.Controller.SetVelocity(direction * StateMachine.Controller.GetMoveSpeed());
        StateMachine.Controller.Animation.Play(animationName, true, onPlayFrame: frame =>
        {
            Debug.Log($"Current Playing Animation: {animationName}");
        });
    }

    public override void _Update()
    {
        // As the MoveSpeed is keep updating in the game, we need to update the velocity every frame
        StateMachine.Controller.SetVelocity(StateMachine.Controller.LastMoveDirection * StateMachine.Controller.GetMoveSpeed());
    }
}
