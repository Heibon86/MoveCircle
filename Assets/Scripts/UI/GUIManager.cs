using System;
using Cysharp.Threading.Tasks;
using Game.Player;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI
{
    public class GUIManager : MonoBehaviour
    {
        [SerializeField] private AssetReference _startScreenPrefab;
        
        private readonly Subject<CallbackClick> _listeners = new Subject<CallbackClick>();
        private CompositeDisposable _disposable = new CompositeDisposable();
        private StartScreen _startScreen;

        public IObservable<CallbackClick> Trigger => _listeners;

        public async UniTask Initialize()
        {
            await LoadStartScreen();
            SubscribeEvents();
        }
        
        public void ShowStartScreen()
        {
            _startScreen.gameObject.SetActive(true);
        }

        private async UniTask LoadStartScreen()
        {
            var result = Addressables.LoadAssetAsync<GameObject>(_startScreenPrefab);
            
            await UniTask.WaitUntil(() => result.IsDone);
        
            if (result.Result.TryGetComponent(out StartScreen startScreen))
            {
                _startScreen = Instantiate(startScreen, transform);
            }
            
            _startScreen.gameObject.SetActive(false);
        }
        
        private void SubscribeEvents()
        {
            _startScreen.Trigger.Where(callback => callback.Key.Equals(KeysStorage.ClickStartButton))
                .Subscribe(ClickStartButton).AddTo(_disposable);
        }

        private void ClickStartButton(CallbackClick callbackClick)
        {
            _startScreen.gameObject.SetActive(false);
            _listeners.OnNext(callbackClick);
        }
    }
}
