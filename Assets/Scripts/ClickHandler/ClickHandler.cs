using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.Player
{
    public class ClickHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private readonly Subject<CallbackClick> _listeners = new Subject<CallbackClick>();
        private CompositeDisposable _disposable = new CompositeDisposable();

        public IObservable<CallbackClick> Trigger => _listeners;
        
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
                    Debug.Log("ClickThis");
                    _listeners.OnNext(new CallbackClick(KeysStorage.ClickPlayer, Input.mousePosition));
                }
            }
            else
            {
                Debug.Log("ClickScreen");
                _listeners.OnNext(new CallbackClick(KeysStorage.ClickScreen, Input.mousePosition));
            }
        }
    }
}
