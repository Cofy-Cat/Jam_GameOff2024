using cfEngine.Util;
using UnityEngine;
using System.Collections.Generic;
public class MoveState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Idle, CharacterStateId.Move, CharacterStateId.Interact };

    public class Param : StateParam
    {
        public Vector2 direction;
    }

    public override CharacterStateId Id => CharacterStateId.Move;

    protected internal override void StartContext(CharacterStateMachine sm, StateParam param)
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
            faceDirection = sm.Controller.LastFaceDirection;
            direction = sm.Controller.LastMoveDirection;
        }

        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.Move, faceDirection);
        sm.Controller.SetVelocity(direction * sm.Controller.moveSpeed);

        sm.Controller.Animation.Play(animationName, true, onPlayFrame: frame =>
        {
            Debug.Log($"Current Playing Animation: {animationName}");
            // if (sm.Controller is PlayerController player && player.moveClip != null)
            // {
            //     AudioManager.Instance.PlaySoundFXClip(player.moveClip, 1);
            // }
        });
    }
}
