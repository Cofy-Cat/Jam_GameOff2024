using cfEngine.Util;
using UnityEngine;
using System.Collections.Generic;

public class InteractState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Idle, CharacterStateId.Move };

    public override CharacterStateId Id => CharacterStateId.Interact;
    
    protected internal override void StartContext(StateParam stateParam)
    {
        StateMachine.Controller.Interacting = true;
        var animationName = AnimationName.GetDirectional(AnimationName.Interact, StateMachine.Controller.LastFaceDirection);
        Debug.Log($"Current Playing Animation: {animationName}");
        StateMachine.Controller.SetVelocity(Vector2.zero);

        // Play the interact animation and set up the onAnimationEnd callback
        StateMachine.Controller.Animation.Play(animationName, false, onAnimationEnd: () =>
        {
            StateMachine.Controller.Interacting = false;

            // Play the next animation here
            // var nextAnimationName = AnimationName.GetDirectional(AnimationName.Idle, sm.Controller.LastFaceDirection);
            // Debug.Log($"-- Interaction Animation ends. Playing Next Animation: {nextAnimationName}");
            // sm.Controller.Animation.Play(nextAnimationName, true);
        });
    }

    protected internal override void OnEndContext()
    {
        // Reset interacting flag when the state ends
    }
}
