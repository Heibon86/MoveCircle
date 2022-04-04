using System;
using Game.Player;
using UniRx;
using UnityEngine;

namespace ClickHandler
{
    public class ClickHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private readonly Subject<Callback> _listeners = new Subject<Callback>();
        private CompositeDisposable _disposable = new CompositeDisposable();

        public IObservable<Callback> Trigger => _listeners;
        
        private void OnEnable()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(xs => Click())
                .AddTo(_disposable);
            
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButton(0))
                .Subscribe(xs => Drag())
                .AddTo(_disposable);
            
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonUp(0))
                .Subscribe(xs => EndDrag())
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        private void Click()
        {
            _listeners.OnNext(new Callback(KeysStorage.StartDrag, Input.mousePosition));
        }

        private void Drag()
        {
            CheckRaycast();
        }

        private void EndDrag()
        {
            _listeners.OnNext(new Callback(KeysStorage.EndDrag, Input.mousePosition));
        }
        
        private void CheckRaycast()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent(out PlayerController playerController))
                {
                    _listeners.OnNext(new Callback(KeysStorage.ClickPlayer, Input.mousePosition));
                }
            }
            else
            {
                _listeners.OnNext(new Callback(KeysStorage.ClickScreen, Input.mousePosition));
            }
        }
    }
}
