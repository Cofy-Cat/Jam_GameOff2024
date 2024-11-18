using cfEngine.Util;
using UnityEngine;
using System.Collections.Generic;
public class EmoState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Move, CharacterStateId.Interact };

    public class Param : StateParam
    {
        public Vector2 direction;
    }

    public override CharacterStateId Id => CharacterStateId.Emo;

    // private float emoMoveSpeed = 3f;

    protected internal override void StartContext(StateParam param)
    {
        StateMachine.Controller.SetVelocity(Vector2.zero);
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.Emo, StateMachine.Controller.LastFaceDirection);
        StateMachine.Controller.Animation.Play(animationName, true);
    }

    protected internal override void OnEndContext()
    {
        StateMachine.Controller.SetEmo(false);
    }
}
