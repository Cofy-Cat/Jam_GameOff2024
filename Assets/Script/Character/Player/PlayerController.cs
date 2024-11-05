using System;
using UnityEngine;
using UnityEngine.InputSystem;
using cfEngine.Util;

public class PlayerController : Controller
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float maxDashClickGap = 0.3f;
    [SerializeField] private float maxComboAttackGap = 0.2f;
    public AudioClip moveClip;

    private Vector2 _lastMoveInput = Vector2.zero;
    public Vector2 LastMoveInput => _lastMoveInput;

    protected override void OnEnable()
    {
        base.OnEnable();
        _input.onActionTriggered += OnActionTriggered;
        _sm.OnAfterStateChange += OnStateChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _input.onActionTriggered -= OnActionTriggered;
        _sm.OnAfterStateChange -= OnStateChanged;
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            // case "Move":
            //     OnMove(context);
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _lastMoveInput = context.ReadValue<Vector2>();

        if (_lastMoveInput != Vector2.zero)
        {
            // _sm.TryGoToState();
        }
        else
        {
            // _sm.TryGoToState();
        }
    }

    private void OnStateChanged(StateChangeRecord<CharacterStateId> stateChange)
    {
        // if (stateChange.LastState == CharacterStateId)
        // {
        //     if (_lastMoveInput != Vector2.zero)
        //     {
        //         // _command.ExecuteCommand(new (_lastMoveInput));
        //     }
        // }
    }
}