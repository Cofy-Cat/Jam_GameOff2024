using cfUnityEngine.Util;

public enum CharacterStateId
{
    Idle,
    Move,
    Hurt,
    Dash,
    Dead
}


public abstract class CharacterState : MonoState<CharacterStateId, CharacterState, CharacterStateMachine> { }

public class CharacterStateMachine : MonoStateMachine<CharacterStateId, CharacterState, CharacterStateMachine>
{
    public Controller Controller;
}