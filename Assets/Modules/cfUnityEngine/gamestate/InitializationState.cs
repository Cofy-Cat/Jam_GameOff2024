using System.Collections.Generic;
using cfEngine.Util;

public class InitializationState: GameState
{
    public override HashSet<GameStateId> Whitelist { get; } = new() { GameStateId.Battle };
    public override GameStateId Id => GameStateId.Initialization;
    protected internal override void StartContext(GameStateMachine gsm, cfEngine.Util.StateParam param)
    {
        Game.Pool.AddPool("Vfx", new PrefabPool<SpriteAnimation>(Game.Asset.Load<SpriteAnimation>("Vfx"), true));
    }
}