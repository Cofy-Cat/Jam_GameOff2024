using cfEngine.Util;
using UnityEngine;
using System.Collections.Generic;
public class ActivateState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Activate, CharacterStateId.Idle, CharacterStateId.Move };

    public class Param : StateParam
    {
        public Vector2 direction;
    }

    public override CharacterStateId Id => CharacterStateId.Activate;

    protected internal override void StartContext(StateParam param)
    {
        StateMachine.Controller.SetVelocity(Vector2.zero);
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.Activate, StateMachine.Controller.LastFaceDirection);
        StateMachine.Controller.Animation.Play(animationName, false, onAnimationEnd: () =>
        {
            StateMachine.Controller.setOpacity(0.5f);
            StateMachine.ForceGoToState(CharacterStateId.Idle);
        });
    }

    protected internal override void OnEndContext()
    {
        StateMachine.Controller.setOpacity(0.5f);
    }
}
