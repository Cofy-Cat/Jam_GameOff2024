using cfUnityEngine.Util;

public enum CharacterStateId
{
    Idle,
    Move,
    Interact,
    Emo,
    Activate

}


public abstract class CharacterState : MonoState<CharacterStateId, CharacterState, CharacterStateMachine> { }

public class CharacterStateMachine : MonoStateMachine<CharacterStateId, CharacterState, CharacterStateMachine>
{
    public Controller Controller;
}