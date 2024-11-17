using cfEngine.Util;

namespace cfUnityEngine.GameState.Bootstrap
{
    public class UILoadState: GameState
    {
        public override GameStateId Id => GameStateId.UILoad;
        protected internal override void StartContext(StateParam param)
        {
            var ui = UI.Instance;

            ui.Register("UI/InventoryUIPanel", new InventoryUIPanel());
        }
    }
}