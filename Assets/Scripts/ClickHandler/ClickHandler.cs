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
                .Subscribe(xs => CheckRaycast())
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
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
