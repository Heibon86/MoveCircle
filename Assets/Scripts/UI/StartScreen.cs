using System;
using Game.Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartScreen : MonoBehaviour
    {
        private readonly Subject<CallbackClick> _listeners = new Subject<CallbackClick>();

        public IObservable<CallbackClick> Trigger => _listeners;

        [SerializeField] private Button _startButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartButton_OnClick);
        }
        
        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartButton_OnClick);
        }

        private void StartButton_OnClick()
        {
            _listeners.OnNext(new CallbackClick(KeysStorage.ClickStartButton, Vector3.zero));
        }
    }
}
