using VContainer;
using VContainer.Unity;

namespace LA.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Player>(Lifetime.Singleton);
            builder.Register<MainGameLoop>(Lifetime.Singleton);
            builder.Register<MainLoopSim>(Lifetime.Singleton);
        }
    }
}