using System;
using UnityEngine;
using UnityEngine.InputSystem;
using cfEngine.Util;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;

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
        var param = new StateParam();
        _sm.ForceGoToState(CharacterStateId.Idle, param);
        // Try to set the rigidbody drag to 0
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
                if(Interacting) return;     
                OnMove(context);
                break;
            case "Interact": 
                OnInteract(context);
                break;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"Input Value of OnMove {context.ReadValue<Vector2>()}");
        if (context.ReadValue<Vector2>().y != 0)
        {
            return;
        }

        _lastMoveInput = context.ReadValue<Vector2>();

        if (_lastMoveInput != Vector2.zero)
        {
            var directionParam = new MoveState.Param
            {
                direction = _lastMoveInput
            };
            _sm.TryGoToState(CharacterStateId.Move, directionParam);
        }
        else
        {
            _sm.TryGoToState(CharacterStateId.Idle, new StateParam());
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        _sm.TryGoToState(CharacterStateId.Interact, new StateParam());
    }

    private void OnStateChanged(StateChangeRecord<CharacterStateId> stateChange)
    {
        if (_lastMoveInput != Vector2.zero)
        {
            Debug.Log($"Changed to state: {stateChange.LastState} to {stateChange.NewState}");
        }
    }

    public void DisableInteracting()
    {
        Interacting = false;
    }
}