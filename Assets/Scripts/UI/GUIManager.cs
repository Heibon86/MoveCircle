using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI
{
    public class GUIManager : MonoBehaviour
    {
        [SerializeField] private AssetReference _startScreenPrefab;

        private StartScreen _startScreen;

        public async void ShowStartScreen()
        {
            var result = Addressables.LoadAssetAsync<GameObject>(_startScreenPrefab);
            
            await UniTask.WaitUntil(() => result.IsDone);
        
            if (result.Result.TryGetComponent(out StartScreen startScreen))
            {
                _startScreen = Instantiate(startScreen, transform);
            }
        }
    }
}
