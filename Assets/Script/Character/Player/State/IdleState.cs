using System.Collections.Generic;
using cfEngine.Util;
using UnityEngine;

public class IdleState : CharacterState
{
    public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Idle, CharacterStateId.Move, CharacterStateId.Interact};
    public override CharacterStateId Id => CharacterStateId.Idle;
    private float _startTime;
    private const float IdleDuration = 10f;

    protected internal override void StartContext(StateParam stateParam)
    {
        string animationName;
        animationName = AnimationName.GetDirectional(AnimationName.Idle, StateMachine.Controller.LastFaceDirection);
        StateMachine.Controller.Animation.Play(animationName, true);
        StateMachine.Controller.SetVelocity(Vector2.zero);

        _startTime = Time.time;
    }

    protected void Update()
    {
        // Check if the elapsed time exceeds the idle duration
        if (Time.time - _startTime > IdleDuration)
        {
            // set the controller isEMo to true
            StateMachine.Controller.SetEmo(true);
            StateMachine.ForceGoToState(CharacterStateId.Emo);
        }
    }
} 