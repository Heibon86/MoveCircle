using System;
using ClickHandler;
using Cysharp.Threading.Tasks;
using Game.Player;
using Installers;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace UI
{
    public class GUIManager : MonoBehaviour
    {
        private const string KeyScore = "Score";
        
        [SerializeField] private AssetReference _startScreenPrefab;
        [SerializeField] private AssetReference _gameScreenViewPrefab;
        
        private readonly Subject<Callback> _listeners = new Subject<Callback>();
        private CompositeDisposable _disposable = new CompositeDisposable();
        private StartScreen _startScreen;
        private GameScreenView _gameScreenView;
        private int _score;

        public IObservable<Callback> Trigger => _listeners;

        public async UniTask Initialize()
        {
            _score = PlayerPrefs.GetInt(KeyScore);
            
            await LoadStartScreen();
            SubscribeEvents();
        }
        
        public void ShowStartScreen()
        {
            _startScreen.gameObject.SetActive(true);
        }

        public async void ShowGameScreenView()
        {
            await LoadGameScreenView();

            PlayerController playerController = CoreSceneInstaller.Context.Container.Resolve<PlayerController>();
            playerController.Trigger.Where(result => result.Key.Equals(KeysStorage.Collision)).Subscribe(SetScore);
        }

        private void SetScore(Callback callback)
        {
            _score++;
            
            PlayerPrefs.SetInt(KeyScore, _score);
            PlayerPrefs.Save();
            
            _gameScreenView.SetScoreText(_score);
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
        
        private async UniTask LoadGameScreenView()
        {
            var result = Addressables.LoadAssetAsync<GameObject>(_gameScreenViewPrefab);
            
            await UniTask.WaitUntil(() => result.IsDone);
        
            if (result.Result.TryGetComponent(out GameScreenView gameScreenView))
            {
                _gameScreenView = Instantiate(gameScreenView, transform);
                _gameScreenView.SetScoreText(_score);
            }
        }
        
        private void SubscribeEvents()
        {
            _startScreen.Trigger.Where(callback => callback.Key.Equals(KeysStorage.ClickStartButton))
                .Subscribe(ClickStartButton).AddTo(_disposable);
        }

        private void ClickStartButton(Callback callback)
        {
            _startScreen.gameObject.SetActive(false);
            _listeners.OnNext(callback);
        }
    }
}
