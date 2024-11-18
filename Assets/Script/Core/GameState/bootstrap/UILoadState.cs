using System.Collections.Generic;
using cfEngine.Util;

namespace cfUnityEngine.GameState.Bootstrap
{
    public class UILoadState: GameState
    {
        public override HashSet<GameStateId> Whitelist { get; } = new()
        {
            GameStateId.Initialization
        };

        public override GameStateId Id => GameStateId.UILoad;
        protected internal override void StartContext(StateParam param)
        {
            var ui = UI.Instance;

            ui.Register("Panel/InventoryPanel", new InventoryPopupPanel());
            
            StateMachine.ForceGoToState(GameStateId.BootstrapEnd);
        }
    }
}