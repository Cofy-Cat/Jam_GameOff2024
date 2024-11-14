using System.Collections.Generic;
using cfEngine.Util;

namespace cfUnityEngine.GameState.Bootstrap
{
    public class UserDataLoadState : GameState
    {
        public override HashSet<GameStateId> Whitelist { get; } = new() { GameStateId.Initialization };

        public override GameStateId Id => GameStateId.UserDataLoad;

        private void RegisterSavables()
        {
            var USER_DATA = Game.UserData;

#if CF_STATISTIC
            USER_DATA.Register(Game.Meta.Statistic);
#endif
#if CF_INVENTORY
            USER_DATA.Register(Game.Meta.Inventory);
#endif
        }
        
        protected internal override void StartContext(StateParam stateParam)
        {
            Game.UserData.LoadInitializeAsync(Game.TaskToken).ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully)
                {
                    StateMachine.TryGoToState(GameStateId.Initialization);
                }
            });
        }
    }
}