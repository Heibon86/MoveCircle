using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game;
using Game.Player;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Installers
{
    public class CoreSceneInstaller : MonoInstaller
    {
        [SerializeField] private ClickHandler _clickHandler;
        [SerializeField] private Camera _camera;
        [SerializeField] private AssetReference _guiManager;
        [SerializeField] private AssetReference _gameController;

        public override async void InstallBindings()
        {
            BindClickHandler();
            BindCamera();
            await BindGameScreen();
            await BindGameController();
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
