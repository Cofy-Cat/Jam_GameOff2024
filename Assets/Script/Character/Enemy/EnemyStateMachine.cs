using cfUnityEngine.Util;

public enum EnemyStateId
{
    EnemyIdle,
    Move,
    Interact,
    TriggerOn,
    TriggerOff,
    Chase,
    Attack
}

public abstract class EnemyState : MonoState<EnemyStateId, EnemyState, EnemyStateMachine> { }

public class EnemyStateMachine : MonoStateMachine<EnemyStateId, EnemyState, EnemyStateMachine>
{
    public EnemyController Controller;
}
