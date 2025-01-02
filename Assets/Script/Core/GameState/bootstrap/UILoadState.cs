using System.Collections.Generic;
using System.Threading.Tasks;
using cfEngine.Extension;
using cfEngine.Logging;
using cfEngine.Util;
using cfUnityEngine.UI;

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
            var ui = UIRoot.Instance;

            ui.Register<InventoryUI>("Panel/InventoryUI");

            var loadTaskList = new List<Task>
            {
                ui.LoadPanel<InventoryUI>()
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