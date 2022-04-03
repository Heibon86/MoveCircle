using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController: MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        [Inject] private ClickHandler _clickHandler;
        [Inject] private Camera _camera;

        public void Initialize()
        {
            _clickHandler.Trigger.Where(result => result.Key.Equals(KeysStorage.ClickPlayer)).Subscribe(ClickThis)
                .AddTo(_disposable);
            _clickHandler.Trigger.Where(result => result.Key.Equals(KeysStorage.ClickScreen)).Subscribe(ClickScreen)
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        private void ClickThis(CallbackClick callbackClick)
        {
           _playerMovement.StopMove();
        }
        
        private void ClickScreen(CallbackClick callbackClick)
        {
            Vector3 point = _camera.ScreenToWorldPoint(callbackClick.Point);
            point.z = 0;
            _playerMovement.MoveToPoint(point);
        }
    }
}
