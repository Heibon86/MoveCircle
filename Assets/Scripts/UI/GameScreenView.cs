using Game.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameScreenView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _distanceText;

        public void SetScoreText(int score)
        {
            _scoreText.text = $"Score: {score}";
        }
        
        public void SetDistanceText(float distanceText)
        {
            _distanceText.text = $"Distance: {distanceText}";
        }
    }
}
