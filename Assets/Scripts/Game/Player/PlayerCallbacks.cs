using System;
using ClickHandler;
using Game.Bonuses;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class PlayerCallbacks : MonoBehaviour
    {
        private readonly Subject<Callback> _listeners = new Subject<Callback>();
        
        public IObservable<Callback> Trigger => _listeners;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent(out SquareBonus squareBonus))
            {
                _listeners.OnNext(new Callback(KeysStorage.Collision));
            }
        }
    }
}
