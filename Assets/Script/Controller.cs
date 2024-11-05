using UnityEngine;

public partial class AnimationName
{
    public const string Idle = nameof(Idle);
    public const string Walk = nameof(Walk);
    public const string HurtLeft = nameof(HurtLeft);
    public const string HurtRight = nameof(HurtRight);    
    public const string Death = nameof(Death);

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
}

public abstract class Controller : MonoBehaviour
{
    [SerializeField] protected Transform _mainCharacter;
    [SerializeField] protected SpriteAnimation _anim;
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected CharacterStateMachine _sm;
    // [SerializeField] protected ActionCommandController _command;
    private Animation shadowAnimation;


    [Header("Stat")]
    public Vector2 moveSpeed = Vector2.one;

    public Vector2 dashSpeed = Vector2.one;

    private float _lastFaceDirection = 0f;
    public float LastFaceDirection => _lastFaceDirection;

    private Vector2 _lastMoveDirection = Vector2.zero;
    public Vector2 LastMoveDirection => _lastMoveDirection;

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }
}

