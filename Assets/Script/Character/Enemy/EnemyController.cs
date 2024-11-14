using cfEngine.Util;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] private GameObject player;
    [SerializeField] public EnemyStateMachine _sm;
    private Controller playerController;
    [SerializeField] public SpriteAnimation Animation;

    public float LastFaceDirection => _lastFaceDirection;
    private float _lastFaceDirection = 0f;
    public Vector2 LastMoveDirection => _lastMoveDirection;
    private Vector2 _lastMoveDirection = Vector2.zero;
    private Vector3 direction;
    private Vector2 input;
    [SerializeField] float chaseRange = 5f;

    protected void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<Controller>();
        Assert.IsNotNull(player);
        Assert.IsNotNull(playerController);
        input = Vector2.zero;
        direction = Vector3.zero;
    }

    private void Start()
    {
        _sm.ForceGoToState(EnemyStateId.EnemyIdle);
    }

    private void FixedUpdate()
    {
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

    public bool ifPlayerIsNear()
    {
        return Vector2.Distance(player.transform.position, transform.position) < chaseRange;
    }
}
