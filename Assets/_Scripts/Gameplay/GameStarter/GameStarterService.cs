using LA.AudioSystem;
using LA.AudioSystem.Database;
using LA.DI;
using LA.UI;
using SW.Utilities.LoadAsset;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LA.Gameplay.GameStarter
{
    public class GameStarterService
    {
        private readonly LifetimeScope _parentScope;
        private LifetimeScope _gameScope;

        private PathConfig _pathConfig;
        private MusicDatabase _musicDatabase;
        private MusicService _musicService;


        public GameStarterService(LifetimeScope parentScope, PathConfig pathConfig, MusicService musicService)
        {
            _parentScope = parentScope;
            _pathConfig = pathConfig;
            _musicService = musicService;
            _musicDatabase = LoadAssetUtility.Load<MusicDatabase>(pathConfig.MusicDatabase);
        }


        public void Load()
        {
            LifetimeScope prefab = LoadAssetUtility.Load<LifetimeScope>(_pathConfig.GameScope);

            _gameScope = _parentScope.CreateChildFromPrefab(prefab);
            _gameScope.Build();
            _musicService.PlayMusic(_musicDatabase.GameMusic, Vector2.zero);
        }


        public void Unload()
        {
            _gameScope?.Dispose();
        }


        public void Reset()
        {
            Unload();
            Load();
        }
    }
}