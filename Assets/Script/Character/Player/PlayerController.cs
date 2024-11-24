using UnityEngine;
using UnityEngine.InputSystem;
using cfEngine.Util;

public class PlayerController : Controller
{
    [SerializeField] private PlayerInput _input;
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
        moveSpeed = new Vector2(5, 0);
        dashSpeed = new Vector2(10, 0);
        Debug.Log("PlayerController Start - moveSpeed: " + moveSpeed + " dashSpeed: " + dashSpeed);
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
        if (Interacting) return;
        switch (context.action.name)
        {
            case "Move":
                OnMove(context);
                break;
            case "Interact":
                OnInteract(context);
                break;
            case "Ability":
                OnAbility(context);
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
        // Physics Get overlapped interactable object
        // Then call the interactable object's interact method

        var interactable = Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Interactable"));
        // Check if the returned object is interactable object 
        if (interactable != null)
        {
            Debug.Log("Interacting with " + interactable.name);
            var interactableComponent = interactable.GetComponent<Interactable>();
            if (interactableComponent != null)
            {
                Debug.Log(interactable.name + " is interactable");
                interactableComponent.Interact(GetComponent<Collider2D>());
            }
        }
    }

    private void OnAbility(InputAction.CallbackContext context)
    {
        _sm.TryGoToState(CharacterStateId.Activate, new ActivateState.Param
        {
            direction = _lastMoveInput
        });
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