using System;
using ClickHandler;
using Game.Bonuses;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController: MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerCallbacks _pLayerCallbacks;
        
        private readonly Subject<Callback> _listeners = new Subject<Callback>();
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        [Inject] private ClickHandler.ClickHandler _clickHandler;
        [Inject] private Camera _camera;
        
        public IObservable<Callback> Trigger => _listeners;

        public void Initialize()
        {
            _clickHandler.Trigger.Where(result => result.Key.Equals(KeysStorage.ClickPlayer)).Subscribe(ClickThis)
                .AddTo(_disposable);
            _clickHandler.Trigger.Where(result => result.Key.Equals(KeysStorage.ClickScreen)).Subscribe(ClickScreen)
                .AddTo(_disposable);
            
            _pLayerCallbacks.Trigger.Where(result => result.Key.Equals(KeysStorage.Collision))
                .Subscribe(callback =>
                {
                    _listeners.OnNext(callback);
                })
                .AddTo(_disposable);
            
            _playerMovement.Trigger.Where(result => result.Key.Equals(KeysStorage.TraveledPath))
                .Subscribe(callback =>
                {
                    _listeners.OnNext(callback);
                })
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        private void ClickThis(Callback callback)
        {
           _playerMovement.StopMove();
        }
        
        private void ClickScreen(Callback callback)
        {
            Vector3 point = _camera.ScreenToWorldPoint(callback.Point);
            point.z = 0;
            _playerMovement.MoveToPoint(point);
        }
    }
}
