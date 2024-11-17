using cfEngine.Util;

namespace cfUnityEngine.GameState.Bootstrap
{
    public class LocalLoadState: GameState
    {
        public override GameStateId Id => GameStateId.LocalLoad;
        protected internal override void StartContext(StateParam param)
        {
            var ui = UI.Instance;
            
            ui.Register("Local/LoadingPanel", new LoadingPanel());
        }
    }
}