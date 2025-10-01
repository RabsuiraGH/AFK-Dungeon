using LA.BattleLog;
using LA.Gameplay.GameLoop;
using LA.Gameplay.Player;
using VContainer;
using VContainer.Unity;

namespace LA.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<BattleLogService>(Lifetime.Singleton);
            builder.Register<Player>(Lifetime.Singleton);
            builder.Register<BattleService>(Lifetime.Singleton);
            builder.Register<GameService>(Lifetime.Singleton);
        }
    }
}