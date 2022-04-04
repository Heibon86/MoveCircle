using System;
using ClickHandler;
using Game.Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartScreen : MonoBehaviour
    {
        private readonly Subject<Callback> _listeners = new Subject<Callback>();

        public IObservable<Callback> Trigger => _listeners;

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
            _listeners.OnNext(new Callback(KeysStorage.ClickStartButton));
        }
    }
}
