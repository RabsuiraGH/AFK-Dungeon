using LA.Gameplay.GameStarter;
using LA.SoundSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LA.DI
{
    public class GlobalLifetimeScope : LifetimeScope
    {
        [SerializeField] private PathConfig _pathConfig;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_pathConfig);

            builder.Register<SoundFXService>(Lifetime.Singleton);
            builder.Register<RandomService>(Lifetime.Singleton).As<IRandomService>();
            builder.Register<GameStarterService>(Lifetime.Singleton);
        }
    }
}