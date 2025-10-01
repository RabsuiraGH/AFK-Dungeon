using UnityEngine;

namespace LA.Gameplay.GameStarter
{
    public class GameStarterKamikaze : MonoBehaviour
    {
        private GameStarterService _gameStarterService;


        [VContainer.Inject]
        public void Construct(GameStarterService gameStarterService)
        {
            _gameStarterService = gameStarterService;
        }


        private void Start()
        {
            _gameStarterService.Load();
            Destroy(this.gameObject);
        }
    }
}