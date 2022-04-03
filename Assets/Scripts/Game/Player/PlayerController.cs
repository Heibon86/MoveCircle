using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController: MonoBehaviour
    {
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        [Inject] private ClickHandler _clickHandler;
        
        private void OnEnable()
        {
            _clickHandler.Trigger.Where(result => result.Key.Equals(KeysStorage.ClickPlayer)).Subscribe(ClickThis)
                .AddTo(_disposable);
            _clickHandler.Trigger.Where(result => result.Key.Equals(KeysStorage.ClickScreen)).Subscribe(ClickScreen)
                .AddTo(_disposable);
        }
        
        private void ClickThis(CallbackClick callbackClick)
        {
            //stop
        }
        
        private void ClickScreen(CallbackClick callbackClick)
        {
            //moveTo
        }
    }
}
