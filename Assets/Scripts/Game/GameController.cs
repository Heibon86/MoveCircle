using System;
using Game.Player;
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

        private void LoadGame(CallbackClick callbackClick)
        {
            
        }
    }
}
