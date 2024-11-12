using cfUnityEngine.Util;

public enum CharacterStateId
{
    Idle,
    Move,
    Interact,
    TriggerOn,
    TriggerOff,
    Chase,
    Attack
}


public abstract class CharacterState : MonoState<CharacterStateId, CharacterState, CharacterStateMachine> { }

public class CharacterStateMachine : MonoStateMachine<CharacterStateId, CharacterState, CharacterStateMachine>
{
    public Controller Controller;
}