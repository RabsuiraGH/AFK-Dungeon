using LA.DI;
using LA.UI;
using SW.Utilities.LoadAsset;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LA
{
    public class GameStarter
    {
        private readonly LifetimeScope _parentScope;
        private LifetimeScope _gameScope;

        private PathConfig _pathConfig;


        public GameStarter(LifetimeScope parentScope, PathConfig pathConfig)
        {
            Debug.Log(($"ParentScope {parentScope.name}"));
            _parentScope = parentScope;
            _pathConfig = pathConfig;
        }


        public void Load()
        {
            Debug.Log(($"Load Game"));

            var prefab = LoadAssetUtility.Load<LifetimeScope>(_pathConfig.GameScope);

            _gameScope = _parentScope.CreateChildFromPrefab(prefab);
        }


        public void Unload()
        {
            Debug.Log(($"Unload Game"));
            _gameScope?.Dispose();
        }


        public void Reset()
        {
            Debug.Log(($"Reset Game"));
            _gameScope?.Dispose();
            Load();
        }
    }
}