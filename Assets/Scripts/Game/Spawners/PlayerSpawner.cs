using Cysharp.Threading.Tasks;
using Game.Player;
using Installers;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private AssetReference _playerControllerAsset;

        private PlayerController _playerController;

        public async void CreatePlayer()
        {
            var result = Addressables.LoadAssetAsync<GameObject>(_playerControllerAsset);
            
            await UniTask.WaitUntil(() => result.IsDone);
        
            if (result.Result.TryGetComponent(out PlayerController playerController))
            {
                _playerController = Instantiate(playerController, transform);
                CoreSceneInstaller.Context.Container.Inject(_playerController);
                _playerController.Initialize();
            }
        }
    }
}
