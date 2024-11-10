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
        // Create a empty param
        var param = new StateParam();
        _sm.ForceGoToState(CharacterStateId.Idle, param);
        Debug.Log($"Rigidbody Drag: {_rb.linearDamping} and Angular Drag: {_rb.angularDamping}");
        _rb.linearDamping = 0f;
        _rb.angularDamping = 0f;
        Debug.Log($"Rigidbody Drag: {_rb.linearDamping} and Angular Drag: {_rb.angularDamping}");

    }

    private void Update()
    {
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
        //Debug Log the input value
        Debug.Log($"Input Value of OnMove {context.ReadValue<Vector2>()}");

        // Ignore up and down arrow inputs
        if (context.ReadValue<Vector2>().y != 0)
        {
            return;
        }

        _lastMoveInput = context.ReadValue<Vector2>();

        if (_lastMoveInput != Vector2.zero)
        {
            // Create the parameter for the MoveState
            var directionParam = new MoveState.Param
            {
                direction = _lastMoveInput
            };
            _sm.TryGoToState(CharacterStateId.Move, directionParam);
            // _sm.ForceGoToState(CharacterStateId.Move, directionParam);
        }
        else
        {
            var param = new StateParam();
            _sm.TryGoToState(CharacterStateId.Idle, param);
            // _sm.ForceGoToState(CharacterStateId.Idle, param);
        }
    }

    private void OnStateChanged(StateChangeRecord<CharacterStateId> stateChange)
    {
        if (_lastMoveInput != Vector2.zero)
        {
            // Print cuurent state
            Debug.Log($"Changed to state: {stateChange.LastState} to {stateChange.NewState}");
        }
    }
}