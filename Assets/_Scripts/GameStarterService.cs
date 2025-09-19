using LA.DI;
using LA.UI;
using SW.Utilities.LoadAsset;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LA
{
    public class GameStarterService
    {
        private readonly LifetimeScope _parentScope;
        private LifetimeScope _gameScope;

        private PathConfig _pathConfig;


        public GameStarterService(LifetimeScope parentScope, PathConfig pathConfig)
        {
            _parentScope = parentScope;
            _pathConfig = pathConfig;
        }


        public void Load()
        {
            LifetimeScope prefab = LoadAssetUtility.Load<LifetimeScope>(_pathConfig.GameScope);

            _gameScope = _parentScope.CreateChildFromPrefab(prefab);
        }


        public void Unload()
        {
            _gameScope?.Dispose();
        }


        public void Reset()
        {
            _gameScope?.Dispose();
            Load();
        }
    }
}