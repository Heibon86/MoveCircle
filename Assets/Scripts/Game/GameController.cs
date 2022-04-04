using System;
using ClickHandler;
using Game.Player;
using Game.Spawners;
using UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        [Inject] private GUIManager _guiManager;
        [Inject] private PlayerSpawner _playerSpawner;
        [Inject] private BonusesSpawner _bonusesSpawner;
        
        public async void Initialize()
        {
            await _guiManager.Initialize();

            _guiManager.Trigger.Where(result => result.Key.Equals(KeysStorage.ClickStartButton)).Subscribe(LoadGame).AddTo(_disposable);

            _guiManager.ShowStartScreen();
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        private async void LoadGame(Callback callback)
        {
            _bonusesSpawner.Initialize();
            await _playerSpawner.CreatePlayer();
            _guiManager.ShowGameScreenView();
        }
    }
}
