using System.Collections.Generic;
using System.Threading.Tasks;
using cfEngine.Logging;
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

            ui.Register<InventoryPopupPanel>("Panel/InventoryPopupPanel");

            var loadTaskList = new List<Task>
            {
                ui.LoadPanel<InventoryPopupPanel>()
            };

            Task.WhenAll(loadTaskList)
                .ContinueWithSynchronized(t =>
                {
                    if (t.IsFaulted)
                    {
                        Log.LogException(t.Exception);
                        return;
                    }
                    StateMachine.ForceGoToState(GameStateId.BootstrapEnd);
                });
        }
    }
}