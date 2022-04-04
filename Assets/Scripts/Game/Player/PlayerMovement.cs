using System;
using ClickHandler;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float SpeedMove = 3f;
        
        [SerializeField] private AnimationCurve _animationCurve;
        
        private readonly Subject<Callback> _listeners = new Subject<Callback>();
        private CompositeDisposable _disposable = new CompositeDisposable();

        private Vector3 _lastPoint;
        private bool _isMove;

        public IObservable<Callback> Trigger => _listeners;

        private void OnEnable()
        {
            Observable.EveryUpdate()
                .Where(_ => _isMove)
                .Subscribe(xs => CheckDistance())
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        private void CheckDistance()
        {
            float distance = Vector3.Distance(_lastPoint, transform.position);
            _listeners.OnNext(new Callback(KeysStorage.TraveledPath, distance));
            
            _lastPoint = transform.position;
        }

        public void MoveToPoint(Vector3 pointTo)
        {
            StopMove();
            
            float duration = CalculateDuration(pointTo);

            _isMove = true;

            _lastPoint = transform.position;
            
            transform.DOMove(pointTo, duration).SetEase(_animationCurve).OnComplete(() =>
            {
                _isMove = false;
            });
        }

        public void MoveToPath(Vector3[] path)
        {
            float duration = CalculateDuration(path);
            transform.DOPath(path, 1).SetEase(_animationCurve);
        }

        public void StopMove()
        {
            DOTween.KillAll();
            _isMove = false;
        }

        private float CalculateDuration(Vector3 pointTo)
        {
            float distance = Vector3.Distance(transform.position, pointTo);
            float duration = distance / SpeedMove;

            return duration;
        }
        
        private float CalculateDuration(Vector3[] path)
        {
            float distance = 0;
            
            for (int i = 0; i < path.Length - 1; i++)
            {
                distance = Vector3.Distance(path[i], path[i + 1]);
            }
            
            float duration = distance / SpeedMove;

            return duration;
        }
    }
}
