using cfEngine.Util;

public class DeadState : CharacterState
{
    public override CharacterStateId Id => throw new System.NotImplementedException();

    protected internal override void StartContext(CharacterStateMachine sm, StateParam param)
    {
        throw new System.NotImplementedException();
    }
}
