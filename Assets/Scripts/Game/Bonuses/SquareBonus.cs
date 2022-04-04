using System;
using ClickHandler;
using Game.Player;
using UniRx;
using UnityEngine;

namespace Game.Bonuses
{
    public class SquareBonus : MonoBehaviour
    {
        private readonly Subject<Callback> _listeners = new Subject<Callback>();

        public IObservable<Callback> Trigger => _listeners;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent(out PlayerController playerController))
            {
                _listeners.OnNext(new Callback(KeysStorage.Collision, this));
            }
        }
    }
}
