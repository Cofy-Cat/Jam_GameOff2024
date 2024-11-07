using System;
using UnityEngine;
using UnityEngine.InputSystem;
using cfEngine.Util;
using DocumentFormat.OpenXml.Drawing.Diagrams;

public class PlayerController : Controller
{
    [SerializeField] private PlayerInput _input;
    // [SerializeField] private float maxDashClickGap = 0.3f;
    // [SerializeField] private float maxComboAttackGap = 0.2f;
    public AudioClip moveClip;

    private Vector2 _lastMoveInput = Vector2.zero;
    public Vector2 LastMoveInput => _lastMoveInput;

    private void Start()
    {
        _sm.ForceGoToState(CharacterStateId.Idle);
    }


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
            case "Move":
                OnMove(context);
                break;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _lastMoveInput = context.ReadValue<Vector2>();

        if (_lastMoveInput != Vector2.zero)
        {
            // Create the parameter for the MoveState
            var directionParam = new MoveState.Param
            {
                direction = _lastMoveInput
            };
            // _sm.TryGoToState(CharacterStateId.Move);
            _sm.ForceGoToState(CharacterStateId.Move, directionParam);
        }
        else
        {
            // _sm.TryGoToState(CharacterStateId.Idle);
            _sm.ForceGoToState(CharacterStateId.Idle);
        }
    }

    private void OnStateChanged(StateChangeRecord<CharacterStateId> stateChange)
    {
        if (_lastMoveInput != Vector2.zero)
        {
            // _command.ExecuteCommand(new (_lastMoveInput));
        }
    }
}