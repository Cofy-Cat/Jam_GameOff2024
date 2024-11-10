using cfEngine.Util;
using UnityEngine;
using System.Collections.Generic;

public class InteractState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Idle, CharacterStateId.Move, CharacterStateId.Interact };

    public override CharacterStateId Id => CharacterStateId.Interact;
    
    protected internal override void StartContext(CharacterStateMachine sm, StateParam param)
    {
        sm.Controller.Interacting = true;
        var animationName = AnimationName.GetDirectional(AnimationName.Interact, sm.Controller.LastFaceDirection);
        Debug.Log($"Current Playing Animation: {animationName}");
        sm.Controller.SetVelocity(Vector2.zero);

        // Play the interact animation and set up the onAnimationEnd callback
        sm.Controller.Animation.Play(animationName, false, onAnimationEnd: () =>
        {
            sm.Controller.Interacting = false;

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
