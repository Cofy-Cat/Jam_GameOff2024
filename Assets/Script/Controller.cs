using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class AnimationName
{
    //Make sure your animation name follow this
    public const string Idle = nameof(Idle);
    public const string Move = nameof(Move);
    public const string Interact = nameof(Interact);
    public const string HurtLeft = nameof(HurtLeft);
    public const string HurtRight = nameof(HurtRight);
    public const string Dash = nameof(Dash);
    public const string Death = nameof(Death);
    public const string TriggerOn = nameof(TriggerOn);
    public const string TriggerOff = nameof(TriggerOff);
    public const string Chase = nameof(Chase);
    public const string Attack = nameof(Attack);
    public const string Emo = nameof(Emo);
    public const string Activate = nameof(Activate);

    public static string GetDirectional(string animationName, float horizontalDirection)
    {
        if (horizontalDirection >= 0)
        {
            return $"{animationName}Right";
        }
        else
        {
            return $"{animationName}Left";
        }
    }

    public static string GetComboDirectional(string animationName, string[] combo, float horizontalDirection)
    {
        var comboString = combo == null ? string.Empty : string.Join("", combo);
        return GetDirectional($"{animationName}{comboString}", horizontalDirection);
    }
}

[Serializable]
public class HealthRecord
{
    public float current;
    public float max;
}

public class ControllerRecord
{
    public Sprite iconSprite;
    public HealthRecord health;
}

public abstract class Controller : MonoBehaviour
{
    // [SerializeField] protected Collider2DComponent _shadow;
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected SpriteAnimation _anim;
    [SerializeField] protected CharacterStateMachine _sm;
    [SerializeField] protected Transform _mainCharacter;

    [Header("Stat")]
    protected Vector2 moveSpeed = Vector2.one;

    protected Vector2 dashSpeed = Vector2.one;

    private float _lastFaceDirection = 0f;
    public float LastFaceDirection => _lastFaceDirection;

    private Vector2 _lastMoveDirection = Vector2.zero;
    public Vector2 LastMoveDirection => _lastMoveDirection;

    [SerializeField] private HealthRecord _health;
    [SerializeField] protected float attackDamage = 10f;
    [SerializeField] protected float attackKnockbackForce = 0.5f;
    [SerializeField] protected Vector2 throwForce = new Vector2(5, 5);
    [SerializeField] protected AudioClip hurtClip;
    [SerializeField] protected Sprite cardIconSprite;

    public ControllerRecord ControllerRecord;
    public event Action<HealthRecord> onHealthChange;
    public event Action onDead;

    #region getter

    public SpriteAnimation Animation => _anim;
    public Rigidbody2D Rigidbody => _rb;
    public Transform MainCharacter => _mainCharacter;
    public HealthRecord Health => _health;

    public bool isDead => _health.current <= 0;

    protected bool isEmo = false;
    protected bool isActivating { get; set; } = false;
    public bool Interacting { get; set; } = false;

    public bool isTriggered { get; set; } = false;

    protected bool IsEmo { get; set; } = false;

    #endregion

    protected virtual void Awake()
    {
        _sm.Controller = this;
        // shadowAnimation = _shadow.GetComponentInChildren<Animation>();

        ControllerRecord = new ControllerRecord()
        {
            health = _health,
            iconSprite = cardIconSprite
        };
    }

    private void Start()
    {
        StartCoroutine(BodyValidation());
    }

    protected virtual void OnEnable()
    {
        // _shadow.triggerEnter += OnShadowTriggerEnter;
        // _shadow.triggerExit += OnShadowTriggerExit;
    }

    protected virtual void OnDisable()
    {
        // _shadow.triggerEnter -= OnShadowTriggerEnter;
        // _shadow.triggerExit -= OnShadowTriggerExit;
    }

    private IEnumerator BodyValidation()
    {
        var checkPeriod = new WaitForSeconds(1f);
        while (!isDead)
        {
            yield return checkPeriod;

            var bodyTransform = _mainCharacter.transform;
            if (bodyTransform.localPosition.y < 0)
            {
                bodyTransform.localPosition = new Vector2(bodyTransform.localPosition.x, 0);
            }
        }
    }

    protected virtual void OnShadowTriggerEnter(Collider2D other)
    {

    }

    protected virtual void OnShadowTriggerExit(Collider2D other)
    {

    }

    public void SetHealthValue(HealthRecord record)
    {
        if (record == null)
        {
            return;
        }
        _health.current = record.current;
        _health.max = record.max;
        onHealthChange?.Invoke(_health);
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rb.linearVelocity = velocity;

        if (velocity != Vector2.zero)
        {
            _lastMoveDirection = velocity.normalized;
        }

        if (!Mathf.Approximately(velocity.x, 0f))
        {
            _lastFaceDirection = Mathf.Sign(velocity.x) * velocity.x / velocity.x;
            Debug.Log($"Last Face Direction: {_lastFaceDirection}");
        }
    }

    public Vector2 GetMoveSpeed()
    {
        return moveSpeed;
    }

    public Vector2 SetMoveSpeed(Vector2 value)
    {
        return moveSpeed = value;
    }

    public Vector2 GetDashSpeed()
    {
        return dashSpeed;
    }

    public Vector2 SetDashSpeed(Vector2 value)
    {
        return dashSpeed = value;
    }

    // Add a public method to set the isEmo property
    public void SetEmo(bool value)
    {
        isEmo = value;
    }

    public void setSpriteOpacity(float value)
    {
        // Traverse the hierarchy to find the SpriteRenderer component
        // PlayerObject (Current script)
        // └── ChildGameObject(has SpriteAnimation script)
        //     ├── SpriteLibrary
        //     ├── SpriteResolver
        //     └── SpriteRenderer

        // Get the GameObject (child of the current GameObject), and then get the SpriteRenderer component
        SpriteRenderer spriteRenderer = _anim.GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1, 1, 1, value);
        }
    }

    // Add public method to get & set the isActivating property
    public bool GetIsActivating()
    {
        return isActivating;
    }

    public void SetIsActivating(bool value)
    {
        isActivating = value;
    }

}
