using cfEngine.Util;
using UnityEngine;

namespace cfUnityEngine.GameState.Bootstrap
{
    public class LocalLoadState: GameState
    {
        public override GameStateId Id => GameStateId.LocalLoad;
        protected internal override void StartContext(StateParam param)
        {
            var uiPrefab = Game.Asset.Load<GameObject>("Local/UIRoot");
            var ui = Object.Instantiate(uiPrefab).GetComponent<UI>();

            var loadingPanel = new LoadingPanel();
            ui.Register("Local/LoadingPanel", loadingPanel);
            loadingPanel.Init().ContinueWithSynchronized(t =>
            {
                loadingPanel.ShowPanel();
                StateMachine.ForceGoToState(GameStateId.InfoLoad);
            }, Game.TaskToken);
            
        }
    }
}