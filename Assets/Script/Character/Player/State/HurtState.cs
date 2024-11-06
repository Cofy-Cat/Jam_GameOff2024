using cfEngine.Util;

public class HurtState : CharacterState
{
    public override CharacterStateId Id => CharacterStateId.Hurt;

    protected internal override void StartContext(CharacterStateMachine sm, StateParam param)
    {
        throw new System.NotImplementedException();
    }
}
