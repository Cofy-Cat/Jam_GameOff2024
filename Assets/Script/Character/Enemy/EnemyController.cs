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

    [Header("Player Detection")]
    [SerializeField] private float raycastOffset = 5f;
    [SerializeField] private float raycastDistance = 45f;
    private Vector2 _rayCastOrigin;


    public float LastFaceDirection => _lastFaceDirection;
    private float _lastFaceDirection = 0f;
    public Vector2 LastMoveDirection => _lastMoveDirection;
    private Vector2 _lastMoveDirection = Vector2.zero;
    private Vector3 direction;
    private Vector2 input;
    [SerializeField] float chaseRange = 7f;

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
        // return Vector2.Distance(player.transform.position, transform.position) < chaseRange;

        var _rayCastOrigin = transform.position;
        var direction = Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(_rayCastOrigin, direction, chaseRange, LayerMask.GetMask("Player"));
        Debug.DrawRay(_rayCastOrigin, direction * chaseRange, Color.green);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player is near");
                return true;
            }
        }

        return false;
    }

    public void changePlayerMoveSpeed()
    {
        if (ifPlayerIsNear())
            playerController.SetMoveSpeed(new Vector2(2, 0));
        else
            playerController.SetMoveSpeed(new Vector2(5, 0));

        Debug.Log("Player Move Speed: " + playerController.GetMoveSpeed());
    }
}
