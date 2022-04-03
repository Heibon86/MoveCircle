using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game;
using Game.Bonuses;
using Game.Player;
using Game.Spawners;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Installers
{
    public class CoreSceneInstaller : MonoInstaller
    {
        public static Context Context { get; private set; }

        [SerializeField] private Context _context;
        [SerializeField] private ClickHandler _clickHandler;
        [SerializeField] private Camera _camera;
        [SerializeField] private AssetReference _guiManager;
        [SerializeField] private AssetReference _gameController;
        [SerializeField] private AssetReference _playerSpawner;
        [SerializeField] private AssetReference _bonusesSpawner;
        [SerializeField] private AssetReference _bonusPool;

        public override async void InstallBindings()
        {
            BindGameArea();
            BindClickHandler();
            BindCamera();
            await BindBonusPool();
            await BindBonusesSpawner();
            await BindPlayerSpawner();
            await BindGameScreen();
            await BindGameController();
            Context = _context;
        }

        private async UniTask BindGameController()
        {
            var result = Addressables.LoadAssetAsync<GameObject>(_gameController);
            
            await UniTask.WaitUntil(() => result.IsDone);
            
            GameController gameController = Container
                 .InstantiatePrefabForComponent<GameController>(result.Result);

            BindAsSingle(gameController);

            gameController.Initialize();
        }

        private async UniTask BindGameScreen()
        {
            var result = Addressables.LoadAssetAsync<GameObject>(_guiManager);
            
            await UniTask.WaitUntil(() => result.IsDone);
            
            GUIManager guiManager = Container
                .InstantiatePrefabForComponent<GUIManager>(result.Result);

            BindAsSingle(guiManager);
        }
        
        private async UniTask BindBonusPool()
        {
            var result = Addressables.LoadAssetAsync<GameObject>(_bonusPool);
            
            await UniTask.WaitUntil(() => result.IsDone);
            
            BonusPool bonusPool = Container
                .InstantiatePrefabForComponent<BonusPool>(result.Result);

            BindAsSingle(bonusPool);
        }
        
        private async UniTask BindBonusesSpawner()
        {
            var result = Addressables.LoadAssetAsync<GameObject>(_bonusesSpawner);
            
            await UniTask.WaitUntil(() => result.IsDone);
            
            BonusesSpawner bonusesSpawner = Container
                .InstantiatePrefabForComponent<BonusesSpawner>(result.Result);

            BindAsSingle(bonusesSpawner);
        }
        
        private async UniTask BindPlayerSpawner()
        {
            var result = Addressables.LoadAssetAsync<GameObject>(_playerSpawner);
            
            await UniTask.WaitUntil(() => result.IsDone);
            
            PlayerSpawner playerSpawner = Container
                .InstantiatePrefabForComponent<PlayerSpawner>(result.Result);

            BindAsSingle(playerSpawner);
        }
        
        private void BindGameArea()
        {
            GameArea gameArea = new GameArea();
            
            BindAsSingle(gameArea);
        }

        private void BindClickHandler()
        {
            BindAsSingle(_clickHandler);
        }
        
        private void BindCamera()
        {
            BindAsSingle(_camera);
        }

        private void BindAsSingle<T>(T obj)
        {
            Container
                .Bind<T>()
                .FromInstance(obj)
                .AsSingle();
        }
    }
}
